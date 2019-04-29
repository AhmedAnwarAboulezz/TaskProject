using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SmartTechTask.Models;
using System.IO;
using System.Data.Entity;

namespace SmartTechTask.Controllers
{
    public class HomeController : Controller
    {
        private ProductContext db = new ProductContext();

        public ActionResult Index()
        {
            ViewBag.Title = "Product List";
            return View(db.Product.ToList());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Product product, HttpPostedFileBase productPhoto)
        {
            if (ModelState.IsValid)
            {
                if (productPhoto != null)
                {
                    String unique = DateTime.Now.ToString("yyyyMMddHHmmssfff");
                    var filename = Path.GetFileName(productPhoto.FileName);
                    var filename2 = Path.GetFileName(productPhoto.FileName);
                    var path = Server.MapPath("~/Content/images/" + unique + filename);
                    var path1 = "Content/images/" + unique + filename;
                    productPhoto.SaveAs(path);
                    product.Photo = path1;
                }
                product.LastUpdated = DateTime.Now;
                db.Product.Add(product);
                db.SaveChanges();
                TempData["productadded"] = "Product Added Successfully";
                return Redirect("~/Home/Index");
            }

            return View(product);
        }

        [HttpPost]
        public ActionResult EditProducts()
        {
            string storeid = Request.Form["newid"];
            int productid = int.Parse(storeid);
            if (productid != null)
            {
                var productdefault = db.Product.Where(c => c.Id == productid).FirstOrDefault();
                string storeName = Request.Form["newname"];
                string storePrice = Request.Form["newprice"];
                HttpPostedFileBase formphoto = Request.Files["imageUploadForm"];

                Product product = db.Product.Find(productid);
                product.Name = storeName;
                product.Price = decimal.Parse(storePrice);
                product.LastUpdated = DateTime.Now;
                if (formphoto != null)
                {
                    var imagepath = productdefault.Photo;
                    string fullPath = Request.MapPath("~/" + imagepath);
                    if (System.IO.File.Exists(fullPath))
                    {
                        System.IO.File.Delete(fullPath);
                    }
                    String unique = DateTime.Now.ToString("yyyyMMddHHmmssfff");
                    var filename = Path.GetFileName(formphoto.FileName);
                    var path = Server.MapPath("~/Content/images/" + unique + filename);
                    var path1 = "Content/images/" + unique + filename;
                    formphoto.SaveAs(path);
                    product.Photo = path1;
                }

                db.Entry(product).State = EntityState.Modified;
                db.SaveChanges();
                return Json(new { names = product.Name, photos = product.Photo, prices = product.Price });
            }
            return Json(new { message = "Something went wrong" });
        }

        public ActionResult ProductDetails(int productid)
        {

            if (productid != null)
            {
                var productdefault = db.Product.Where(c => c.Id == productid).FirstOrDefault();

                return Json(new { names = productdefault.Name, photos = productdefault.Photo, prices = productdefault.Price, proids = productdefault.Id });
            }
            return Json(new { message = "Something went wrong" });
        }
        public ActionResult DeleteProduct(int productid)
        {

            if (productid != null)
            {
                var productdefault = db.Product.Where(c => c.Id == productid).FirstOrDefault();
                var imagepath = productdefault.Photo;
                string fullPath = Request.MapPath("~/" + imagepath);
                if (System.IO.File.Exists(fullPath))
                {
                    System.IO.File.Delete(fullPath);
                }
                db.Product.Remove(productdefault);
                db.SaveChanges();
                return Json(new { message = "This Product ' " + productdefault.Name + " ' deleted successfully from your products list" });
            }
            return Json(new { message = "Something went wrong" });
        }

    }
}