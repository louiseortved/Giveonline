using System;
using Giveonline.Models;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Giveonline.ViewModels;
using Microsoft.Ajax.Utilities;

namespace Giveonline.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext db = new ApplicationDbContext();


        public ActionResult Index()
        {
            ContentVM vm = new ContentVM()
            {
                Sliders = db.Sliders.OrderByDescending(x => x.Id).Take(4).OrderBy(x => x.Id).ToList(),
                Events = db.Events.OrderBy(x => x.DateOfEvent).Take(3).ToList(),
                Abouts = db.About.ToList(),


            };
            return View(vm);

        }

        public ActionResult GIVEonline()
        {
            List<Donation> donations = db.Donations.ToList();
            return View(donations);

        }


        public ActionResult UpcommingEvents()
        {
            List<Event> events = db.Events.ToList();
            return View(events);

        }

        public ActionResult StoriesFromEvent()
        {
            List<Story> stories = db.Stories.OrderBy(x => x.Event.Title).ToList();
            return View(stories);

        }


        public ActionResult Shop()
        {
            List<Product> products = db.Products.ToList();

            return View(products);
        }

        public ActionResult About()
        {
            List<About> abouts = db.About.ToList();

            return View(abouts);
        }


        public ActionResult _AddSubscriber()
        {
            return PartialView();
        }

        [HttpPost]
        public ActionResult AddSubscriber(string Email, string Name)
        {
            Subscriber sub = new Subscriber
            {
                Email = Email,
                Name = Name,


            };

            db.Subscribers.Add(sub);
            db.SaveChanges();

            return RedirectToAction("Index");
        }


        public ActionResult _UpcommingEvent()
        {
            List<Event> events = db.Events.OrderBy(x => x.DateOfEvent).Take(1).ToList();
           
        return PartialView(events);
    }



    #region Search


    [HttpGet]
    public ActionResult Search(string query)
    {
        var vm = new SearchVM();
        //multiple split words
        //var split = new[] {  };
        if (!string.IsNullOrEmpty(query))
        {
            var array = query.ToLower().Split(new[] { " ", "," }, StringSplitOptions.RemoveEmptyEntries);
            //check if query is null
            foreach (var search in array)
            {

                //find songs "%LIKE%" query
                vm.Abouts.AddRange(db.About
                    .Where(s => s.Title.Contains(search) || s.Description.Contains(search))
                    .ToList());                                                                                // to list da den ellers retunerer en ienumerable
                                                                                                               //find albums "%LIKE%" query
                vm.Donations.AddRange(db.Donations
                    .Where(a => a.Title.Contains(search)).ToList());
                //find artist "%LIKE%" query
                vm.Events.AddRange(db.Events
                    //.Where(a => a.Active)
                    .Where(a => a.Title.Contains(search) || a.Description.Contains(search) || a.Location.Contains(search))                //er også en mulighed 
                    .ToList());
                    //find genre "%LIKE%" query
                    vm.Products.AddRange(db.Products

                        .Where(p => p.Title.Contains(search) || p.Description.Contains(search))
                        .ToList());
                }


            vm.Abouts = vm.Abouts.DistinctBy(x => x.Title).ToList();
            vm.Donations = vm.Donations.DistinctBy(x => x.Title).ToList();
            vm.Events = vm.Events.DistinctBy(x => x.DateOfEvent).ToList();
            vm.Products = vm.Products.DistinctBy(x => x.Title).ToList();

        }



        return View(vm);
    }
    #endregion



}
}