using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using lm.test.admin.Models.DotnetCore.Blog;

namespace lm.test.admin.Controllers
{
    public class BlogsController : Controller
    {
        private readonly BloggingContext _context;

        public BlogsController(BloggingContext context)
        {
            _context = context;
        }

        // GET: Blogs
        public async Task<PartialViewResult> Index()
        {
            return PartialView(await _context.Blogs.ToListAsync());
        }

        // GET: Blogs/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return PartialView(NotFound());
            }

            var blog = await _context.Blogs
                .SingleOrDefaultAsync(m => m.BlogId == id);
            if (blog == null)
            {
                return PartialView(NotFound());
            }

            return PartialView(blog);
        }

        // GET: Blogs/Create
        public PartialViewResult Create()
        {
            return PartialView();
        }

        // POST: Blogs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public async Task<PartialViewResult> Create([Bind("BlogId,Url")] Blog blog)
        {
            if (ModelState.IsValid)
            {
                _context.Add(blog);
                await _context.SaveChangesAsync();
                return PartialView("Index", _context.Blogs.ToList());
            }
            return PartialView(blog);
        }

        // GET: Blogs/Edit/5
        public async Task<PartialViewResult> Edit(int? id)
        {
            if (id == null)
            {
                return PartialView(NotFound());
            }

            var blog = await _context.Blogs.SingleOrDefaultAsync(m => m.BlogId == id);
            if (blog == null)
            {
                return PartialView(NotFound());
            }
            return PartialView(blog);
        }

        // POST: Blogs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public async Task<PartialViewResult> Edit(int id, [Bind("BlogId,Url")] Blog blog)
        {
            if (id != blog.BlogId)
            {
                return PartialView(NotFound());
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(blog);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BlogExists(blog.BlogId))
                    {
                        return PartialView(NotFound());
                    }
                    else
                    {
                        throw;
                    }
                }
                return PartialView("Index", _context.Blogs.ToList());
            }
            return PartialView(blog);
        }

        // GET: Blogs/Delete/5
        public async Task<PartialViewResult> Delete(int? id)
        {
            if (id == null)
            {
                return PartialView(NotFound());
            }

            var blog = await _context.Blogs
                .SingleOrDefaultAsync(m => m.BlogId == id);
            if (blog == null)
            {
                return PartialView(NotFound());
            }

            return PartialView(blog);
        }

        // POST: Blogs/Delete/5
        [ActionName("DeleteConfirmed")]
        public async Task<PartialViewResult> DeleteConfirmed(int id)
        {
            var blog = await _context.Blogs.SingleOrDefaultAsync(m => m.BlogId == id);
            _context.Blogs.Remove(blog);
            await _context.SaveChangesAsync();
            return PartialView("Index", await _context.Blogs.ToListAsync());
        }

        private bool BlogExists(int id)
        {
            return _context.Blogs.Any(e => e.BlogId == id);
        }
    }
}
