﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CasaDaHora.Domain.Comment;
using CasaDaHorta.Repository.Context;
using CasaDaHora.Domain.Account;
using CasaDaHora.Domain.Post;
using CasaDaHora.Domain.Comment.Repository;

namespace CasaDaHorta.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommentsController : ControllerBase
    {
        private readonly CasaDaHortaContext _context;

        public CommentsController(CasaDaHortaContext context)
        {
            _context = context;
        }

        // GET: api/Comments
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Comments>>> GetComments()
        {
            return await _context.Comments.Include(x => x.Account).ToListAsync();
        }

        // GET: api/Comments/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Comments>> GetComments(Guid id)
        {
            var comments = await _context.Comments.Include(x => x.Account)
                                                  .Include(x => x.Post)
                                                  .FirstOrDefaultAsync(x => x.Id == id);
            var accountResponse = new AccountResponse { Id = comments.Account.Id, Nome = comments.Account.Nome };
            var postViewModel = new PostViewModel { Id = comments.Post.Id };
            var commentResponse = new CommentsResponse { Id = comments.Id, 
                                                        Account = accountResponse, 
                                                        Content = comments.Content, 
                                                        Post = postViewModel 
                                                        };
            if (comments == null)
            {
                return NotFound();
            }

            return comments;
        }

        // PUT: api/Comments/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutComments(Guid id, Comments comments)
        {
            if (id != comments.Id)
            {
                return BadRequest();
            }

            _context.Entry(comments).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CommentsExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Comments
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Comments>> PostComments(Comments comments)
        {
            comments.Account = _context.Accounts.FirstOrDefault(x => x.Id == comments.Account.Id);
            comments.Post = _context.Posts.FirstOrDefault(x => x.Id == comments.Post.Id);
            comments.Id = Guid.Empty;
            _context.Comments.Add(comments);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetComments", new { id = comments.Id }, comments);
        }

        // DELETE: api/Comments/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Comments>> DeleteComments(Guid id)
        {
            var comments = await _context.Comments.FindAsync(id);
            if (comments == null)
            {
                return NotFound();
            }

            _context.Comments.Remove(comments);
            await _context.SaveChangesAsync();

            return comments;
        }

        private bool CommentsExists(Guid id)
        {
            return _context.Comments.Any(e => e.Id == id);
        }
    }
}
