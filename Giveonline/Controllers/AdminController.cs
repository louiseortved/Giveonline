using Giveonline.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http.Headers;
using System.Net.Mail;
using System.Text;
using System.Web;
using System.Web.Mvc;
using Giveonline.Extensions;
using Giveonline.ViewModels;

namespace Giveonline.Controllers
{   /*[Authorize]*/
    public class AdminController : Controller
    {

        private readonly ApplicationDbContext db = new ApplicationDbContext();

        // GET: Admin

        public ActionResult Index()
        {
            return View();
        }



        #region Admin

        public ActionResult AdminProfile()
        {
            return View();
        }

        #endregion

        #region Login

        [AllowAnonymous]
        public ActionResult Login()
        {
            return View();
        }

        #endregion


        #region Products

        public ActionResult ProductList()
        {

            //ProductVM vm = new ProductVM()
            //{
            //    Categories = db.Categories.ToList(),
            //    SizeProduct = db.SizeProducts.ToList()
            //};

            List<Category> allCategories = db.Categories.ToList();

            return View(allCategories);
        }



        [HttpGet]
        public ActionResult ProductCreate()
        {
            ProductVM vm = new ProductVM()
            {
                Categories = db.Categories.ToList(),
                Sizes = db.Sizes.ToList(),
                Brands = db.Brands.ToList()
            };


            return View(vm);
        }



        [HttpPost]
        public ActionResult ProductCreate(string Title, string Description, decimal Price, int Amount, int BrandId, string OnSale,
            int CategoryId, string ImageUrl, int[] SizeId, HttpPostedFileBase file, IEnumerable<HttpPostedFileBase> files)
        {

            var fileComplete = "";
            if (file != null)
            {

                var fileExtension = Path.GetExtension(file.FileName);
                var fileName = Guid.NewGuid().ToString();
                fileComplete = fileName + fileExtension;
                var path = Path.Combine(Server.MapPath("/Uploads/"), fileComplete);
                file.SaveAs(path);
            }

            Product prod = new Product
            {
                Title = Title,
                Description = Description,
                Price = Price,
                InStock = false,
                ImageUrl = file.FileName
            };


            if (Amount > 0)
            {
                prod.InStock = true;
            }

            prod.Amount = Amount;
            prod.OnSale = false;
            if (OnSale == "on")
            {
                prod.OnSale = true;
            }

            prod.BrandId = BrandId;
            prod.CategoryId = CategoryId;
            prod.ImageUrl = fileComplete;

            db.Products.Add(prod);
            db.SaveChanges();


            foreach (var item in files)
            {
                if (item != null && item.ContentLength > 0)
                {
                    var filename = Path.GetFileName(item.FileName);
                    var path = Path.Combine(Server.MapPath("/Uploads/"), filename);
                    item.SaveAs(path);

                    ProductImage img = new ProductImage()
                    {
                        ImgUrl = filename,
                        ProductId = prod.Id
                    };

                    db.ProductImages.Add(img);
                    db.SaveChanges();
                }
            }

            foreach (var item in SizeId)
            {
                SizeProduct size = new SizeProduct()

                {
                    SizeId = item,
                    ProductId = prod.Id
                };


                db.SizeProducts.Add(size);
                db.SaveChanges();
            }


            //foreach (var item in HttpPostedFileBase)
            //{
            //    Image img = new Image()

            //    {
            //     ImgUrl = filename,

            //    };


            //    db.SizeProducts.Add(img);
            //    db.SaveChanges();
            //}

            return RedirectToAction("ProductList");
        }


        [HttpGet]
        public ActionResult
            ProductEdit(int? id) //id´et laves nullable - så kan vi lave en if sætning (tjekke på) om id´et er der.
        {
            ProductVM vm = new ProductVM()
            {
                Categories = db.Categories.ToList(),
                Sizes = db.Sizes.ToList(),
                Product = db.Products.Find(id)
            };
            return View(vm);
        }


        [HttpPost]
        public ActionResult
            ProductEdit(int id, string Title, string Description, decimal Price, int Amount, string OnSale,
                int CategoryId, string ImageUrl, int[] SizeId, HttpPostedFileBase file)
        {

            Product prod = db.Products.Find(id);

            prod.Title = Title;
            prod.Description = Description;
            prod.Price = Price;
            prod.InStock = false;
            prod.OnSale = false;
            if (Amount > 0)
            {
                prod.InStock = true;
            }

            prod.Amount = Amount;
            if (OnSale == "on")
            {
                prod.OnSale = true;
            }
            //prod.OnSale = OnSale;

            db.SaveChanges();

            //return RedirectToAction("ProductEdit", new {id = @prod.Id});      // til eventuelt fejlhåndtering
            return RedirectToAction("ProductList");

        }



        public ActionResult ProductDelete(int id)
        {
            Product prod = db.Products.Find(id);

            db.Products.Remove(prod);
            db.SaveChanges();

            return RedirectToAction("ProductList");
        }


        #endregion


        #region Category

        public ActionResult Category()
        {
            List<Category> allCategories = db.Categories.ToList();
            return View(allCategories);
        }



        [HttpGet]
        public ActionResult CategoryEdit(int? id)
        {
            Category cat = db.Categories.Find(id);
            return View(cat);
        }

        [HttpPost]
        public ActionResult CategoryEdit(int? id, string Title)
        {
            Category cat = db.Categories.Find(id);

            cat.Title = Title;

            db.SaveChanges();
            return RedirectToAction("Category");
        }

        [HttpGet]
        public ActionResult CategoryCreate()
        {

            return View();
        }



        [HttpPost]
        public ActionResult CategoryCreate(string Title)
        {

            Category cat = new Category();

            cat.Title = Title;


            db.Categories.Add(cat);
            db.SaveChanges();
            return RedirectToAction("Category");
        }


        public ActionResult CategoryDelete(int id)
        {
            Category cat = db.Categories.Find(id);

            db.Categories.Remove(cat);
            db.SaveChanges();

            return RedirectToAction("Category");

        }

        #endregion


        #region Brand
        public ActionResult Brand()
        {
            List<Brand> allBrands = db.Brands.ToList();
            return View(allBrands);
        }


        //[HttpGet]
        //public ActionResult CategoryEdit(int? id)
        //{
        //    Category cat = db.Categories.Find(id);
        //    return View(cat);
        //}

        //[HttpPost]
        //public ActionResult CategoryEdit(int? id, string Title)
        //{
        //    Category cat = db.Categories.Find(id);

        //    cat.Title = Title;

        //    db.SaveChanges();
        //    return RedirectToAction("Category");
        //}

        [HttpGet]
        public ActionResult BrandCreate()
        {

            return View();
        }



        [HttpPost]
        public ActionResult BrandCreate(string Title, string ImageUrl, HttpPostedFileBase file)
        {

            var fileComplete = "";
            if (file != null)
            {

                var fileExtension = Path.GetExtension(file.FileName);
                var fileName = Guid.NewGuid().ToString();
                fileComplete = fileName + fileExtension;
                var path = Path.Combine(Server.MapPath("/Uploads/"), fileComplete);
                file.SaveAs(path);
            }


            Brand brand = new Brand()
            {

                Title = Title,
                ImageUrl = ImageUrl,
            };

            brand.ImageUrl = fileComplete;

            db.Brands.Add(brand);
            db.SaveChanges();
            return RedirectToAction("Brand");
        }


        //public ActionResult CategoryDelete(int id)
        //{
        //    Category cat = db.Categories.Find(id);

        //    db.Categories.Remove(cat);
        //    db.SaveChanges();

        //    return RedirectToAction("Category");

        //}


        #endregion


        #region Size

        public ActionResult Size()
        {
            List<Size> allSizes = db.Sizes.ToList();
            return View(allSizes);
        }


        [HttpGet]
        public ActionResult SizeEdit(int? id)
        {
            Size size = db.Sizes.Find(id);
            return View(size);
        }

        [HttpPost]
        public ActionResult SizeEdit(int? id, string Title)
        {
            Size size = db.Sizes.Find(id);

            size.Title = Title;

            db.SaveChanges();
            return RedirectToAction("Size");
        }

        [HttpGet]
        public ActionResult SizeCreate()
        {

            return View();
        }



        [HttpPost]
        public ActionResult SizeCreate(string Title)
        {

            Size size = new Models.Size();

            size.Title = Title;


            db.Sizes.Add(size);
            db.SaveChanges();
            return RedirectToAction("Size");
        }


        public ActionResult SizeDelete(int id)
        {
            Size size = db.Sizes.Find(id);

            db.Sizes.Remove(size);
            db.SaveChanges();

            return RedirectToAction("Size");

        }

        #endregion


        #region Newsletter



        public ActionResult Newsletter()
        {
            List<Newsletter> allNewsletters = db.Newsletters.ToList();
            return View(allNewsletters);
        }


        [HttpGet]
        public ActionResult NewsletterCreate()
        {

            return View();
        }


        [HttpPost]
        [ValidateInput(false)]
        public ActionResult NewsletterCreate(string Title, string HtmlContent)
        {

            Newsletter newsletter = new Newsletter()
            {
                Title = Title,
                HtmlContent = HtmlContent,

            };

            db.Newsletters.Add(newsletter);
            db.SaveChanges();

            return RedirectToAction("Newsletter");
        }



        public ActionResult NewsletterDelete(int id)
        {
            Newsletter news = db.Newsletters.Find(id);


            db.Newsletters.Remove(news);
            db.SaveChanges();

            return RedirectToAction("Newsletter");
        }




        [HttpGet]
        [ValidateInput(false)]
        public ActionResult NewsletterSend(int? id)
        {
            NewsletterVM vm = new NewsletterVM()   //en instansiering af newsletter
            {
                Newsletter = db.Newsletters.Find(id),
                Subscribers = db.Subscribers.ToList()
            };

            return View(vm);
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult NewsletterSend(string[] Email, string HtmlContent, string Title)
        {

            foreach (var item in Email)
            {
                MailMessage message = new MailMessage();

                message.From = new MailAddress("jingle_dk@hotmail");
                message.To.Add(new MailAddress(item));

                message.IsBodyHtml = true;
                message.BodyEncoding = Encoding.UTF8;
                message.Subject = Title;
                message.Body = HtmlContent;

                SmtpClient client = new SmtpClient();
                client.Send(message);
            }



            return RedirectToAction("Newsletter");
        }


        #endregion


        #region Orders

        public ActionResult Order()
        {
            return View();
        }


        public ActionResult OrdreDetail()
        {
            return View();
        }

        #endregion


        #region Events

        public ActionResult EventList()
        {
            List<Event> allEvents = db.Events.ToList();             

            return View(allEvents);
        }

        [HttpGet]
        public ActionResult EventCreate()
        {
          

            return View();
        }

        [HttpPost]
        public ActionResult EventCreate(string Title, string Description, DateTime DateOfEvent, string Location,
            string ImageUrl, HttpPostedFileBase file)
        {

            var fileComplete = "";
            if (file != null)
            {

                var fileExtension = Path.GetExtension(file.FileName);
                var fileName = Guid.NewGuid().ToString();
                fileComplete = fileName + fileExtension;
                var path = Path.Combine(Server.MapPath("/Uploads/"), fileComplete);
                file.SaveAs(path);
            }

        
            Event eve = new Event
            {
                Title = Title,
                Description = Description,   
                DateOfEvent = DateOfEvent,
                Location = Location,
                ImageUrl = file.FileName
            };


            eve.ImageUrl = fileComplete;

            db.Events.Add(eve);
            db.SaveChanges();

            return RedirectToAction("EventList");
        }

        [HttpGet]
        public ActionResult EventEdit(int? id)
        {
            Event eve = db.Events.Find(id);
            return View(eve);
        }

        [HttpPost]
        public ActionResult EventEdit(int id, string Title, string Description, DateTime DateOfEvent, string Location,
            string ImageUrl, HttpPostedFileBase file)
        {
            
            Event eve = db.Events.Find(id);

            eve.Title = Title;
            eve.DateOfEvent = DateOfEvent;
            eve.Description = Description;
            eve.Location = Location;

            db.SaveChanges();
            return RedirectToAction("EventList");
        }

        public ActionResult EventDelete(int id)
        {
            Event eve = db.Events.Find(id);

            db.Events.Remove(eve);
            db.SaveChanges();

            return RedirectToAction("EventList");
        }

        #endregion

        #region Stories

        public ActionResult StoriesList()
        {
            List<Story> allStories = db.Stories.ToList(); 
               
            return View(allStories);
        }

        #endregion

        #region Donations

        public ActionResult DonationList()
        {
            List<Donation> allDonations = db.Donations.ToList();
           
            return View(allDonations);
        }

        [HttpGet]
        public ActionResult DonationCreate()
        {

            return View();
        }

        [HttpPost]
        public ActionResult DonationCreate(string Title, /*decimal Amount,*/
            string ImageUrl, HttpPostedFileBase file)
        {

            var fileComplete = "";
            if (file != null)
            {

                var fileExtension = Path.GetExtension(file.FileName);
                var fileName = Guid.NewGuid().ToString();
                fileComplete = fileName + fileExtension;
                var path = Path.Combine(Server.MapPath("/Uploads/"), fileComplete);
                file.SaveAs(path);
            }

            Donation donation = new Donation
         
            {
                Title = Title,
                ImageUrl = file.FileName
            };


            donation.ImageUrl = fileComplete;

            db.Donations.Add(donation);
            db.SaveChanges();

            return RedirectToAction("DonationList");
        }

        [HttpGet]
        public ActionResult DonationEdit(int? id)
        {
            Donation donation = db.Donations.Find(id);
            return View(donation);
        }

        [HttpPost]
        public ActionResult DonationEdit(int id, string Title, /*decimal Amount,*/
            string ImageUrl, HttpPostedFileBase file)
        {

            Donation donation = db.Donations.Find(id);

            donation.Title = Title;
            

            db.SaveChanges();
            return RedirectToAction("DonationList");
        }

        public ActionResult DonationDelete(int id)
        {
           
            Donation donation = db.Donations.Find(id);
            db.Donations.Remove(donation);
            db.SaveChanges();

            return RedirectToAction("DonationList");
        }




        #endregion


        #region Content(AboutUs)
        public ActionResult ContentList()
        {
          
            List<About> allAbouts = db.About.ToList();

            return View(allAbouts);
        }


        [HttpGet]
        public ActionResult ContentCreate()
        {
            return View();
        }

        [HttpPost]
        public ActionResult ContentCreate(string Title, string Description)
        {

         About about = new About
        
            {
                Title = Title,
                Description = Description,
            
            };


            db.About.Add(about);
            db.SaveChanges();

            return RedirectToAction("ContentList");
        }

        [HttpGet]
        public ActionResult ContentEdit(int? id)
        {
            About about = db.About.Find(id);
        
            return View(about);
        }

        [HttpPost]
        public ActionResult ContentEdit(int id, string Title, string Description)
        {

            About about = db.About.Find(id);       
            about.Title = Title;
            about.Description = Description;
            

            db.SaveChanges();
            return RedirectToAction("ContentList");
        }


        public ActionResult ContentDelete(int id)
        {

            About about = db.About.Find(id);

            db.About.Remove(about);
            db.SaveChanges();

            return RedirectToAction("ContentList");
        }

        #endregion


        #region slider

        public ActionResult Slider()
        {
            List<Slider> allSliders = db.Sliders.ToList();
            return View(allSliders);
        }

        [HttpPost]
        public ActionResult Slider(IEnumerable<HttpPostedFileBase> files)
        {

            if (files.Count() > 0)
            {
                foreach (var image in db.Sliders)
                {
                   
                    Helper.DeleteFile(image.ImageUrl, "/Uploads/slider");
                    //db.Sliders.Remove(image);
                    //db.SaveChanges();

                }
            }
            foreach (var item in files)
            {
                if (item != null && item.ContentLength > 0)
                {
                    var filename = Path.GetFileName(item.FileName);
                    var path = Path.Combine(Server.MapPath("/Uploads/slider"), filename);
                    item.SaveAs(path);

                    Slider img = new Slider()
                    {
                        ImageUrl = filename,
                      
                    };

                    db.Sliders.Add(img);
                    db.SaveChanges() ;
                       
                }
            }
            return RedirectToAction("Slider");
        }



        public ActionResult SliderCreate()
        {
            return View();
        }

        public ActionResult EditSlider()
        {
            return View();
        }

        #endregion
    }



}