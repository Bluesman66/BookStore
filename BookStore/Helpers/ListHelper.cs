﻿using System.Web.Mvc;

namespace BookStore.Helpers
{
	public static class ListHelper
	{
		public static MvcHtmlString ShowList(this HtmlHelper html, string[] items, object htmlAttributes = null)
		{
			TagBuilder ul = new TagBuilder("ul");
			foreach (string item in items)
			{
				TagBuilder li = new TagBuilder("li");
				li.SetInnerText(item);
				ul.InnerHtml += li.ToString();
			}
			ul.MergeAttributes(HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes));

			return new MvcHtmlString(ul.ToString());
		}
	}
}