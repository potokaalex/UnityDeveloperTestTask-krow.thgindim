using System;
using Client.Code.Core;
using Client.Code.Core.BehaviorTree;
using Client.Code.Core.UI;
using Client.Code.Gameplay.Currency;
using Client.Code.Gameplay.CustomerZone;
using Client.Code.Gameplay.Kitchen;
using Client.Code.Gameplay.Player;
using Client.Code.Gameplay.Player.Wallet;
using Client.Code.Gameplay.Restaurant;
using UnityEngine;
using UnityEngine.AI;

namespace Client.Code.Gameplay.Customer
{
    public class CustomerController : MonoBehaviour
    {
        public NavMeshAgent NavMeshAgent;
        public GameObject FoodTray;
        public GameObject GiveMoneyIndicator;
        public TimerView TimerView;
        public ToCameraRotator ToCameraRotator;
        public float CreateOrderTime;
        public float EatTime;
        public CurrencyAmount InitialMoneyPayed;
        private RestaurantController _restaurantController;
        private KitchenController _kitchenController;
        private CustomerTableController _customerTable;
        private CustomerWanderingNode _wanderingNode;
        private CustomerHelper _helper;
        private FoodCreationOrder _currentOrder;
        private PlayerScore _playerScore;
        private PlayerWallet _playerWallet;
        private CustomerZoneController _customerZoneController;
        private INode _tree;
        private CurrencyAmount _moneyPayed;

        public bool CanGoRestaurant => !_helper.GoingToRestaurant;

        public void Construct(RestaurantController restaurantController, CameraController cameraController, KitchenController kitchenController,
            PlayerScore playerScore, PlayerWallet playerWallet, CustomerZoneController customerZoneController, Vector3 areaMin, Vector3 areaMax)
        {
            _playerWallet = playerWallet;
            _kitchenController = kitchenController;
            _restaurantController = restaurantController;
            _playerScore = playerScore;
            _customerZoneController = customerZoneController;
            ToCameraRotator.Construct(cameraController);
            _helper = new CustomerHelper(NavMeshAgent);
            _wanderingNode = new CustomerWanderingNode(_helper, areaMin, areaMax);
        }

        public void Initialize()
        {
            FoodTray.SetActive(false);
            GiveMoneyIndicator.SetActive(false);
            TimerView.Hide();
            _wanderingNode.Initialize();

            _tree = new RepeatNode(new SequenceNode(
                _wanderingNode,
                TryMoveToReception(),
                MoveToCooks(),
                CreateOrder(),
                MoveToTable(),
                WaitOrder(),
                MoveToCooks(),
                GetOrder(),
                MoveToTable(),
                Eat(),
                GiveMoney(),
                LeaveRestaurant()
            ));
        }

        public void Update() => _tree.Tick();

        public void GoRestaurant(CustomerTableController customerTable)
        {
            _customerTable = customerTable;
            _helper.GoingToRestaurant = true;
        }

        private INode TryMoveToReception()
        {
            return new SelectorNode(
                new SequenceNode(
                    new ConditionNode(() => _customerZoneController.ReceptionTable.IsBuilt),
                    _helper.MoveTo(() => _customerZoneController.ReceptionTable.CustomerPoint.position),
                    new ActionNode(() => TimerView.Show()),
                    new TimerNode(() => _customerZoneController.ReceptionTable.ServiceTime,
                        t => { TimerView.View(_customerZoneController.ReceptionTable.ServiceTime, t); }, () =>
                        {
                            TimerView.Hide();
                            _moneyPayed = InitialMoneyPayed;
                            _moneyPayed.Count *= 2;
                        })
                ),
                new ActionNode(() => _moneyPayed = InitialMoneyPayed)
            );
        }

        private INode LeaveRestaurant()
        {
            return new SequenceNode(
                _helper.MoveTo(() => _restaurantController.ExitPoint.position),
                new ActionNode(() => _helper.GoingToRestaurant = false)
            );
        }

        private INode MoveToCooks() => _helper.MoveTo(() => _kitchenController.CooksPoint.position);

        private INode CreateOrder()
        {
            return new SequenceNode(
                new ActionNode(TimerView.Show),
                new TimerNode(() => CreateOrderTime, t => { TimerView.View(CreateOrderTime, t); }, () =>
                {
                    TimerView.Hide();
                    _currentOrder = _kitchenController.CreateOrder();
                })
            );
        }

        private INode MoveToTable() => _helper.MoveTo(() => _customerTable.CustomerPoint.position);

        private INode WaitOrder()
        {
            return new SequenceNode(
                new WaitUntilNode(() =>
                {
                    if (_currentOrder.ProgressTime > 0)
                        TimerView.Show();
                    TimerView.View(_currentOrder.CookingTime, _currentOrder.ProgressTime);
                    if (_currentOrder.IsReady)
                    {
                        TimerView.Hide();
                        return true;
                    }

                    return false;
                })
            );
        }

        private INode GetOrder() => new ActionNode(() => FoodTray.SetActive(true));

        private INode Eat()
        {
            return new SequenceNode(
                new ActionNode(() =>
                {
                    FoodTray.SetActive(false);
                    TimerView.Show();
                }),
                new TimerNode(() => EatTime, t => { TimerView.View(EatTime, t); }, TimerView.Hide)
            );
        }

        private INode GiveMoney()
        {
            IDisposable subscription = null;
            var canGiveMoney = false;

            return new SequenceNode(
                new ActionNode(() =>
                {
                    GiveMoneyIndicator.SetActive(true);
                    subscription = _customerTable.OnInteract.Subscribe(() => canGiveMoney = true);
                    canGiveMoney = false;
                }),
                new WaitUntilNode(() =>
                {
                    if (canGiveMoney)
                    {
                        subscription.Dispose();
                        GiveMoneyIndicator.SetActive(false);
                        _customerTable.Clear();
                        _playerWallet.Add(_moneyPayed);
                        _playerScore.Add(1);
                        return true;
                    }

                    return false;
                })
            );
        }
    }
}