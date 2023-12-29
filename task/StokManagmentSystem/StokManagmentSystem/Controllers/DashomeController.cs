using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using StokManagmentSystem.Service;
using StokManagmentSystem.Models;


namespace StokManagmentSystem.Controllers
{
    public class DashomeController : Controller
    {
        //// GET: Dashome
        private ProductService productService = new ProductService();


        public ActionResult Index()
        {

            return View();
        }

        public ActionResult SaveProduct()
        {

            return View();
        }

        // POST: Dashome/SaveProduct
        // Handles the form submission to save product information
        [HttpPost]
        public ActionResult SaveProduct(ProductModel productinfo)
        {

            if (ModelState.IsValid)
            {
                bool result = productService.SaveProductInfo(productinfo);
                if (result)
                {
                    return RedirectToAction("ListProduct");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Product Alredy Exit");
                }
            }

            return View(productinfo);
        }

        // GET: Dashome/ListProduct
        // Displays the list of all products
        [HttpGet]
        public ActionResult ListProduct()
        {
            try
            {

                List<ProductModel> productList = productService.GetAllProductInfo();

                if (productList != null && productList.Any())
                {
                    return View(productList);
                }
                else
                {
                    return View("Error");
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, "No products found.");
                Console.WriteLine($"Exception: {ex.Message}");

                return View("Error");
            }
        }


        public ActionResult EditProductinfo()
        {
            return View();
        }

        // GET: Dashome/EditProductinfo/{id}
        // Displays the view for editing specific product information
        [HttpGet]
        public ActionResult EditProductinfo(int id)
        {
            var productList = productService.GetAllProductInfoById(id);
            if (productList != null)
            {

                return View(productList);
            }
            else
            {
                return View();
            }
        }
        // POST: Dashome/EditProductinfo/{id}
        // Handles the form submission to update product information
        [HttpPost]
        public ActionResult EditProductinfo(int id, ProductModel productinfo)
        {

            if (ModelState.IsValid)
            {
                bool result = productService.EditProductInfo(id, productinfo);
                if (result)
                {
                    return RedirectToAction("ListProduct");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Cant Update product item");
                }
            }

            return View(productinfo);
        }

        
        public ActionResult DeleteProduct()
        {
            return View();
        }

        // GET: Dashome/DeleteProduct/{id}
        // Handles the deletion of a specific product
        [HttpGet]
        public ActionResult DeleteProduct(int id)
        {
            bool result = productService.DeleteProductInfo(id);
            if (ModelState.IsValid)
            {

                if (result)
                {
                    return RedirectToAction("ListProduct");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Cant delete product item");
                }
            }

            return View();
        }



        // GET: Dashome/ViewDetails/{id}
        // Displays detailed information for a specific product
        [HttpGet]
        public ActionResult ViewDetails(int id)
        {
            var productList = productService.GetAllProductInfoById(id);
            if (productList != null)
            {

                return View(productList);
            }
            else
            {
                return View();
            }
        }

    }
}