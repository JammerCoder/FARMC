using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using FARMC.DAL;
using FARMC.Models;
using FARMC.Web.ViewModels;

namespace FARMC.Web.Controllers
{
    public class FisherfolksController : Controller
    {
        private FARMCDbContext _farmcDbContext;

        public FisherfolksController()
        {
            _farmcDbContext = new FARMCDbContext();
        }

        public ActionResult Index()
        {
            return View(_farmcDbContext.Fisherfolks.ToList());
        }
        
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Fisherfolk fisherfolk = _farmcDbContext.Fisherfolks.Find(id);
            if (fisherfolk == null)
            {
                return HttpNotFound();
            }

            FisherfolkViewModel fisherfolkViewModel =
                ViewModels.Helpers.CreateFisherfolkViewModelFromFisherfolk(fisherfolk);
            fisherfolkViewModel.MessageToUser = string.Format("{0}'s basic information.",fisherfolk.LastName);

            return View(fisherfolkViewModel);
        }
        
        public ActionResult Create()
        {
            FisherfolkViewModel fisherfolkViewModel = new FisherfolkViewModel();
            fisherfolkViewModel.ObjectState = ObjectState.Added;
            return View(fisherfolkViewModel);
        }

        
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Fisherfolk fisherfolk = _farmcDbContext.Fisherfolks.Find(id);
            if (fisherfolk == null)
            {
                return HttpNotFound();
            }

            FisherfolkViewModel fisherfolkViewModel =
                ViewModels.Helpers.CreateFisherfolkViewModelFromFisherfolk(fisherfolk);

            fisherfolkViewModel.MessageToUser = string.Format("{0}'s basic information.", fisherfolkViewModel.LastName);
            
            return View(fisherfolkViewModel);
        }

        
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Fisherfolk fisherfolk = _farmcDbContext.Fisherfolks.Find(id);
            if (fisherfolk == null)
            {
                return HttpNotFound();
            }

            FisherfolkViewModel fisherfolkViewModel = ViewModels.Helpers.CreateFisherfolkViewModelFromFisherfolk(fisherfolk);
            fisherfolkViewModel.MessageToUser = string.Format("{0}'s basic information is about to be deleted permanently!", fisherfolkViewModel.LastName);
            fisherfolkViewModel.ObjectState = ObjectState.Deleted;

            return View(fisherfolkViewModel);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _farmcDbContext.Dispose();
            }
            base.Dispose(disposing);
        }

       [HandleModelStateException]
       public JsonResult Save(FisherfolkViewModel fisherfolkViewModel)
        {
            /*if (!ModelState.IsValid)
            {
                throw new ModelStateException(ModelState);
            }*/
            
           Fisherfolk fisherfolk = ViewModels.Helpers.CreateFisherfolkFromFisherfolkViewModel(fisherfolkViewModel);

            _farmcDbContext.Fisherfolks.Attach(fisherfolk);

            if (fisherfolk.ObjectState == ObjectState.Deleted)
            {
                //Delete Parent with Children
                foreach (FishCatchViewModel fishCatchViewModel in fisherfolkViewModel.FishCatches)
                {
                    FishCatch fishCatch = _farmcDbContext.FishCatches.Find(fishCatchViewModel.FishCatchId);
                    if (fishCatch != null)
                        fishCatch.ObjectState = ObjectState.Deleted;
                }
            }
            else
            {
                //Delete Transaction for Child Entity during Edits 
                foreach (int fishCatchId in fisherfolkViewModel.FishCatchesToDelete)
                {
                    FishCatch fishCatch = _farmcDbContext.FishCatches.Find(fishCatchId);
                    if (fishCatch != null)
                        fishCatch.ObjectState = ObjectState.Deleted;
                }
            }
            
            _farmcDbContext.ApplyStateChanges();
           string messageToUser = string.Empty;

           try
           {
               _farmcDbContext.SaveChanges();
           }
           catch (DbUpdateConcurrencyException)
           {
               messageToUser = 
                   "Another user had made modification to this information. The following are the updated data.";
           }
           catch (Exception ex)
           {
               throw new ModelStateException(ex);
           }
            
           if (fisherfolk.ObjectState == ObjectState.Deleted)
               return Json(new {newLocation = "/Fisherfolks/Index"});

           if (messageToUser.Trim().Length == 0)
               messageToUser = ViewModels.Helpers.GetMessageToUser(fisherfolkViewModel.ObjectState, fisherfolk.LastName);

           fisherfolkViewModel.FisherfolkId = fisherfolk.FisherfolkId;
           _farmcDbContext.Dispose();
           _farmcDbContext = new FARMCDbContext();
           fisherfolk = _farmcDbContext.Fisherfolks.Find(fisherfolkViewModel.FisherfolkId);
           
           fisherfolkViewModel = ViewModels.Helpers.CreateFisherfolkViewModelFromFisherfolk(fisherfolk);
           fisherfolkViewModel.MessageToUser = messageToUser;

           return Json(new {fisherfolkViewModel});
        }
    }
}
