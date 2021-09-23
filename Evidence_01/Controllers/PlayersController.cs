
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Evidence_01.Models;
using Evidence_01.Models.ViewModels;

namespace work_01.Controllers
{
    [Authorize]
    public class PlayersController : Controller
    {
        ClubDbContext db = new ClubDbContext();
        // GET: Players
        [AllowAnonymous]
        public ActionResult Index(int page = 1)
        {
            var players = db.Players.Include("Sports");
            return View(players.OrderBy(x => x.PlayerId).ToList());
        }
        public ActionResult Create()
        {
            ViewBag.Sports = new SelectList(db.Sports, "SportsId", "SportsName");
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(PlayersViewModel evm)
        {
            if (ModelState.IsValid)
            {
                if (evm.Picture != null)
                {
                    string filepath = Path.Combine("~/Images", Guid.NewGuid().ToString() + Path.GetExtension(evm.Picture.FileName));
                    evm.Picture.SaveAs(Server.MapPath(filepath));

                    Player player = new Player
                    {
                        PlayerName = evm.PlayerName,
                        JoinDate = evm.JoinDate,
                        Grade = evm.Grade,
                        SportsId = evm.SportsId,
                        PicturePath = filepath
                    };
                    db.Players.Add(player);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            ViewBag.Sports = new SelectList(db.Sports, "SportsId", "SportsName");
            return View(evm);
        }
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Player player = db.Players.Find(id);
            if (player == null)
            {
                return HttpNotFound();
            }
            PlayersViewModel evm = new PlayersViewModel
            {
                PlayerId = player.PlayerId,
                PlayerName = player.PlayerName,
                JoinDate = player.JoinDate,
                Grade = player.Grade,
                SportsId = player.SportsId,
                PicturePath = player.PicturePath
            };
            ViewBag.Sports = new SelectList(db.Sports, "SportsId", "SportsName", evm.SportsId);
            return View(evm);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(PlayersViewModel evm)
        {
            if (ModelState.IsValid)
            {
                string filepath = evm.PicturePath;
                if (evm.Picture != null)
                {
                    filepath = Path.Combine("~/Images", Guid.NewGuid().ToString() + Path.GetExtension(evm.Picture.FileName));
                    evm.Picture.SaveAs(Server.MapPath(filepath));

                    Player player = new Player
                    {
                        PlayerId = evm.PlayerId,
                        PlayerName = evm.PlayerName,
                        JoinDate = evm.JoinDate,
                        Grade = evm.Grade,
                        SportsId = evm.SportsId,
                        PicturePath = filepath
                    };
                    db.Entry(player).State = System.Data.Entity.EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                else
                {
                    Player player = new Player
                    {
                        PlayerId = evm.PlayerId,
                        PlayerName = evm.PlayerName,
                        JoinDate = evm.JoinDate,
                        Grade = evm.Grade,
                        SportsId = evm.SportsId,
                        PicturePath = filepath
                    };
                    db.Entry(player).State = System.Data.Entity.EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            ViewBag.Sports = new SelectList(db.Sports, "SportsId", "SportsName", evm.SportsId);
            return View(evm);
        }
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Player player = db.Players.Find(id);
            if (player == null)
            {
                return HttpNotFound();
            }
            return View(player);
        }
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Player player = db.Players.Find(id);

            string file_name = player.PicturePath;
            string path = Server.MapPath(file_name);
            FileInfo file = new FileInfo(path);
            if (file.Exists)//check file exsit or not  
            {
                file.Delete();
            }
            db.Players.Remove(player);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

    }
}