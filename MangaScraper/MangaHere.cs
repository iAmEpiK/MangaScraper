﻿using System;

/* Richard Tran
 * Manga Scraper
 * MangaHere.cs
 * 02/2015
 * https://github.com/RichardTran93/MangaScraper
 */
/// <summary>
/// Summary description for Class1
/// </summary>
namespace MangaScraper
{
    public class MangaHere : Manga
    {
        public MangaHere()
        {

        }
        public string extractJPGFromHTML(string html)
        {
            // string[] split = Regex.Split(html,"<img src=\"");//get the URL but contains ""
            int index = html.IndexOf("img src=\"");//get first instance of img src=" at where i is
            html = html.Substring(index + 9); // 9 is for each character of img src="
            index = html.IndexOf("\""); // remove second " at the end of jpg url
            html = html.Substring(0, index - 1);
            return html;
        }
        public string getNextURL(string html)
        {
            string htmlBackup = html;
            int index = html.IndexOf("return next_page();"); // get next page url

            html = html.Substring(0, index); // 45 to get rid of the html, goes straight to the url

            index = html.LastIndexOf("a href=\"");
            html = html.Substring(index + 8); // get rid of a href="
            index = html.IndexOf("\"");
            html = html.Substring(0, index);

            if (html == "javascript:void(0);")//if end of chapter, get next chapter
                html = getNextChapter(htmlBackup);

            return html;
        }
        public string getNextChapter(string html)
        {
            int index = html.IndexOf("Next Chapter:");//get index of next chapter url
            if (index == -1)
            {
                return "null";
            }

            html = html.Substring(index);
            index = html.IndexOf("\"");//start html at first "
            html = html.Substring(index + 1);//get rid of the first "
            index = html.IndexOf("\"");// end html at next "
            html = html.Substring(0, index);


            return html;
        }
        public string getSeriesName(string html)
        {
            int index = html.IndexOf("var series_name");
            string series = html.Substring(index);
            index = series.IndexOf("\"");
            series = series.Substring(index + 1);
            index = series.IndexOf("\"");
            series = series.Substring(0, index);
            return series;
        }

        public string getChapter(string html)
        {
            //MessageBox.Show("getting shit");
            //System.Diagnostics.Debug.Write(html);
            int index = html.IndexOf("var current_chapter");
            string chapter = html.Substring(index); // remove first "
            index = chapter.IndexOf("\"");
            chapter = chapter.Substring(index + 1); //remove first " c###"
            index = chapter.IndexOf("\"");
            chapter = chapter.Substring(0, index); // remove second " now all that is left is c###
            //System.Diagnostics.Debug.Write(chapter);
            return chapter;
        }

        public string getPage(string html)
        {
            int index = html.IndexOf("var current_page");
            string page = html.Substring(index);
            index = page.IndexOf(" ");//current_page = _20_;
            page = page.Substring(index + 1);
            index = page.IndexOf(" "); // pages are in format _##_ where _ is space
            page = page.Substring(index + 1);// =_20_
            index = page.IndexOf(" ");
            page = page.Substring(index + 1);//20_
            index = page.IndexOf(" ");
            page = page.Substring(0, index);//20

            // System.Diagnostics.Debug.Write(page+"\n");
            return page;
        }


    }
}