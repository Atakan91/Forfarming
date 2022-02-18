#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Forfarming.Entities;
using Forfarming.Models;
using Microsoft.AspNetCore.Authorization;

namespace Forfarming.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize]
    public class UsersController : ControllerBase
    {
        private readonly ApplicationContext _context;

        public UsersController(ApplicationContext context)
        {
            _context = context;
        }

        // Tüm Kullanıcıları Listeler
        [HttpGet("getall")]
        public async Task<IActionResult> GetAll()
        {
            return  Ok(await _context.User.ToListAsync());
        }

        //KUllanıcı Bazlı Listeleme Yapar
        [HttpGet("getbyId")]
        public async Task<IActionResult> GetById(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _context.User
                .FirstOrDefaultAsync(m => m.Id == id);
            if (user == null)
            {
                return NotFound();
            }

            return Ok(user);
        }

        //Yeni Kullanıcı Ekleme İşlemi
        [HttpPost("add")]
        public async Task<IActionResult> Create(User user)
        {
            if (ModelState.IsValid)
            {
                _context.Add(user);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(GetAll));
            }
            return Ok(user);
        }


        //Kullanıcı Güncelleme İşlemi
        [HttpPut("put")]
        public async Task<IActionResult> Put(User user)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(user);
                    await _context.SaveChangesAsync();
                    return Ok(user);
                }
                catch (Exception ex)
                {
                    return BadRequest();
                }
                
            }
            return BadRequest();
        }

        //Kullanıcı Silme İşlemi
        [HttpDelete("delete")]
        public async Task<IActionResult> Delete(int id)
        {
            var user = await _context.User.FindAsync(id);
            _context.User.Remove(user);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(GetAll));
        }


    }
}
