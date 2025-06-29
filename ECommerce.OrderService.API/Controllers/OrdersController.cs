using ECommerce.OrderService.Application.DTOs;
using ECommerce.OrderService.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.OrderService.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class OrdersController(IOrderService orderService) : ControllerBase
    {
        /// <summary>
        /// Yeni sipariş oluşturma endpointi
        /// </summary>
        /// <param name="orderDto"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> CreateOrder([FromBody] CreateOrderDto orderDto)
        {
            try
            {
                var createdOrder = await orderService.CreateOrderAsync(orderDto);
                return CreatedAtAction(nameof(GetOrderById), new { id = createdOrder.Id }, createdOrder);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Kullanıcının tüm siparişlerini getirme endpointi
        /// </summary>
        /// <param name="customerId"></param>
        /// <returns></returns>
        [HttpGet("{customerId}")]
        public async Task<IActionResult> GetOrdersByCustomerId(int customerId)
        {
            try
            {
                var orders = await orderService.GetOrderByCustomerIdAsync(customerId);
                return Ok(orders);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        /// <summary>
        /// Sipariş detaylarını getirme endpointi
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetOrderById(int id)
        {
            try
            {
                var order = await orderService.GetOrderByIdAsync(id);
                return Ok(order);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        /// <summary>
        /// Sipariş silme endpointi
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOrder(int id)
        {
            try
            {
                await orderService.DeleteOrderById(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
