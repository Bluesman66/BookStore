using BookStore.Models;
using BookStore.Util;
using System;
using System.Collections.Generic;
using System.Text;
using System.Web.Mvc;

using System.Data.Entity;

namespace BookStore.Controllers
{
	public class HomeController : Controller
	{
		// создаем контекст данных
		BookContext db = new BookContext();

		protected override void Dispose(bool disposing)
		{
			db.Dispose();
			base.Dispose(disposing);
		}

		public ActionResult Index()
		{
			// получаем из бд все объекты Book
			IEnumerable<Book> books = db.Books;
			// передаем все объекты в динамическое свойство Books в ViewBag
			ViewBag.Books = books;			
			// возвращаем представление
			return View();
		}

		[HttpGet]
		public ActionResult Buy(int id)
		{
			ViewBag.BookId = id;
			return View();
		}
		[HttpPost]
		public string Buy(Purchase purchase)
		{
			purchase.Date = DateTime.Now;
			// добавляем информацию о покупке в базу данных
			db.Purchases.Add(purchase);
			// сохраняем в бд все изменения
			db.SaveChanges();
			return "Спасибо," + purchase.Person + ", за покупку!";
		}

		public string Square(int a = 10, int h = 3)
		{
			double s = a * h / 2;
			return "<h2>Площадь треугольника с основанием " + a +
				   " и высотой " + h + " равна " + s + "</h2>";
		}

		public string Square2()
		{
			int a = Int32.Parse(Request.Params["a"]);
			int h = Int32.Parse(Request.Params["h"]);
			double s = a * h / 2;
			return "<h2>Площадь треугольника с основанием " + a + 
				   " и высотой " + h + " равна " + s + "</h2>";
		}

		public ActionResult GetHtml()
		{
			return new HtmlResult("<h2>Привет мир!</h2>");
		}

		public ActionResult GetImage()
		{
			string path = "../Images/autumn.jpg";
			return new ImageResult(path);
		}

		public ActionResult _Partial()
		{
			ViewBag.Message = "Это частичное представление.";
			return PartialView();
		} 

		[HttpGet]
		public ActionResult MultiSelect()
		{
			return View();
		}

		[HttpPost]
		public string MultiSelect(string[] countries)
		{			
			StringBuilder sb = new StringBuilder(); 
			foreach (string country in countries)			
				sb.Append(country).Append(';');

			return sb.ToString();
		}

		[HttpGet]
		public ActionResult SubmitButtons()
		{
			return View();
		}

		[HttpPost]
		public string SubmitButtons(string product, string action)
		{
			if (action == "add")
				return product + " " + "add";
			else if (action == "delete")
				return product + " " + "delete";
			else
				return string.Empty;
		}

		public ActionResult Helpers()
		{
			ViewBag.Message = "Это вызов частичного представления из обычного.";
			return View();
		}

		public ActionResult FormElements()
		{
			return View();
		}

		public ActionResult BookView(int id)
		{			
			var book = db.Books.Find(id);
			return View(book);			
		}

		[HttpGet]
		public ActionResult BookEdit(int? id)
		{
			if (id == default(int?))
				return new HttpNotFoundResult();

			var book = db.Books.Find(id);
			if (book != null)
				return View(book);

			return new HttpNotFoundResult();
		}

		[HttpPost]
		public ActionResult BookEdit(Book book)
		{
			db.Entry(book).State = EntityState.Modified;
			db.SaveChanges();
			return RedirectToAction("Index");
		}
	}
}