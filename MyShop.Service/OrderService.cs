using MyShop.Data.Infrastructure;
using MyShop.Data.Models;
using MyShop.Data.Repositories;
using System;
using System.Collections.Generic;

namespace MyShop.Service
{
    public interface IOrderService
    {
        Order Create(ref Order order, List<OrderDetail> orderDetails);
        void UpdateStatus(int orderId);
        void Save();
    }
    public class OrderService : IOrderService
    {
        private IUnitOfWork _unitOfWork;
        private IOrderRepository _orderRepository;
        private IOrderDetailRepository _orderDetailRepository;

        public OrderService(IUnitOfWork unitOfWork,
            IOrderRepository orderRepository,
            IOrderDetailRepository orderDetailRepository)
        {
            this._unitOfWork = unitOfWork;
            this._orderRepository = orderRepository;
            this._orderDetailRepository = orderDetailRepository;
        }

        public Order Create(ref Order order, List<OrderDetail> orderDetails)
        {
            try
            {
                _orderRepository.Add(order);
                _unitOfWork.Commit();
                foreach (var orderDetail in orderDetails)
                {
                    orderDetail.OrderID = order.ID;
                    _orderDetailRepository.Add(orderDetail);
                }
                return order;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void Save()
        {
            _unitOfWork.Commit();
        }

        public void UpdateStatus(int orderId)
        {
            var order = _orderRepository.GetSingleById(orderId);
            order.Status = true;
            _orderRepository.Update(order);
        }
    }
}
