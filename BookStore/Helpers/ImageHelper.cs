using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BookStore.Helpers
{
	public static class ImageHelper
	{
		public static MvcHtmlString Image(this HtmlHelper html, string src, string alt, int height, int width)
		{
			TagBuilder img = new TagBuilder("img");
			img.MergeAttribute("src", src);
			img.MergeAttribute("alt", alt);
			img.MergeAttribute("height", Convert.ToString(height));
			img.MergeAttribute("width", Convert.ToString(width));

			return MvcHtmlString.Create(img.ToString(TagRenderMode.SelfClosing));
		} 
	}
}