﻿using AutoMapper;
using AutoMapper.QueryableExtensions;
using MealOrdering.Server.Data.Context;
using MealOrdering.Server.Services.Infrastruce;
using MealOrdering.Shared.DTO;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MealOrdering.Server.Services.Services
{
    public class OrderService : IOrderService
    {
        private readonly MealOrderingDbContext context;
        private readonly IMapper mapper;

        public OrderService(MealOrderingDbContext Context, IMapper Mapper)
        {
            context = Context;
            mapper = Mapper;
        }


        #region Order Methods


        #region Get

        public async Task<List<OrderDTO>> GetOrders(DateTime OrderDate)
        {
            var list = await context.Orders.Include(i => i.Supplier)
                      .Where(i => i.CreateDate.Date == OrderDate.Date)
                      .ProjectTo<OrderDTO>(mapper.ConfigurationProvider)
                      .OrderBy(i => i.CreateDate)
                      .ToListAsync();

            return list;
        }

        public async Task<OrderDTO> GetOrderById(Guid Id)
        {
            return await context.Orders.Where(i => i.Id == Id)
                      .ProjectTo<OrderDTO>(mapper.ConfigurationProvider)
                      .FirstOrDefaultAsync();
        }

        #endregion

        #region Post

        public async Task<OrderDTO> CreateOrder(OrderDTO Order)
        {
            var dbOrder = mapper.Map<Data.Models.Orders>(Order);
            await context.AddAsync(dbOrder);
            await context.SaveChangesAsync();

            return mapper.Map<OrderDTO>(dbOrder);
        }

        public async Task<OrderDTO> UpdateOrder(OrderDTO Order)
        {
            var dbOrder = await context.Orders.FirstOrDefaultAsync(i => i.Id == Order.Id);
            if (dbOrder == null)
                throw new Exception("Sipariş Bulunamadı");

            mapper.Map(Order, dbOrder);
            await context.SaveChangesAsync();

            return mapper.Map<OrderDTO>(dbOrder);
        }

        public async Task DeleteOrder(Guid OrderId)
        {
            var detailCount = await context.OrderItems.Where(i => i.OrderId == OrderId).CountAsync();

            if (detailCount > 0)
                throw new Exception($"Bu siparişe ait {detailCount} adet alt sipariş var.");

            var order = await context.Orders.FirstOrDefaultAsync(i => i.Id == OrderId);
            if (order == null)
                throw new Exception("Sipariş bulunamadı");

            context.Orders.Remove(order);

            await context.SaveChangesAsync();
        }

        #endregion

        #endregion


        #region OrderItem Methods

        #region Get

        public async Task<List<OrderItemsDTO>> GetOrderItems(Guid OrderId)
        {
            return await context.OrderItems.Where(i => i.OrderId == OrderId)
                      .ProjectTo<OrderItemsDTO>(mapper.ConfigurationProvider)
                      .OrderBy(i => i.CreateDate)
                      .ToListAsync();
        }

        public async Task<OrderItemsDTO> GetOrderItemsById(Guid Id)
        {
            return await context.OrderItems.Include(i => i.Order).Where(i => i.Id == Id)
                      .ProjectTo<OrderItemsDTO>(mapper.ConfigurationProvider)
                      .FirstOrDefaultAsync();
        }

        #endregion

        #region Post


        public async Task<OrderItemsDTO> CreateOrderItem(OrderItemsDTO OrderItem)
        {
            var order = await context.Orders
                .Where(i => i.Id == OrderItem.OrderId)
                .Select(i => i.ExpireDate)
                .FirstOrDefaultAsync();

            if (order == null)
                throw new Exception("İlgili Siparişin Ana Kaydı Bulunamadı.");

            if (order <= DateTime.Now)
                throw new Exception("Kapanmış Siparişe Yeni Giriş Yapılamaz!!!");


            var dbOrder = mapper.Map<Data.Models.OrderItems>(OrderItem);
            await context.AddAsync(dbOrder);
            await context.SaveChangesAsync();

            return mapper.Map<OrderItemsDTO>(dbOrder);
        }

        public async Task<OrderItemsDTO> UpdateOrderItem(OrderItemsDTO OrderItem)
        {
            var dbOrder = await context.OrderItems.FirstOrDefaultAsync(i => i.Id == OrderItem.Id);
            if (dbOrder == null)
                throw new Exception("Sipariş Bulunamadı");

            mapper.Map(OrderItem, dbOrder);
            await context.SaveChangesAsync();

            return mapper.Map<OrderItemsDTO>(dbOrder);
        }

        public async Task DeleteOrderItem(Guid OrderItemId)
        {
            var orderItem = await context.OrderItems.FirstOrDefaultAsync(i => i.Id == OrderItemId);
            if (orderItem == null)
                throw new Exception("Sipariş Detayı Bulunamadı");

            context.OrderItems.Remove(orderItem);

            await context.SaveChangesAsync();
        }

        #endregion

        #endregion
    }
}