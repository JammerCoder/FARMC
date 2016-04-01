using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FARMC.Models;

namespace FARMC.Web.ViewModels
{
    public static class Helpers
    {
        public static FisherfolkViewModel CreateFisherfolkViewModelFromFisherfolk(Fisherfolk fisherfolk)
        {
            FisherfolkViewModel fisherfolkViewModel = new FisherfolkViewModel();
            fisherfolkViewModel.FisherfolkId = fisherfolk.FisherfolkId;
            fisherfolkViewModel.FisherId = fisherfolk.FisherId;
            fisherfolkViewModel.LastName = fisherfolk.LastName;
            fisherfolkViewModel.ObjectState = ObjectState.Unchanged;

            foreach (FishCatch fishCatch in fisherfolk.FishCatches)
            {
                FishCatchViewModel fishCatchViewModel = new FishCatchViewModel();
                fishCatchViewModel.FishCatchId = fishCatch.FishCatchId;
                fishCatchViewModel.FishName = fishCatch.FishName;
                fishCatchViewModel.Qty = fishCatch.Qty;
                fishCatchViewModel.UnitPrice = fishCatch.UnitPrice;

                fishCatchViewModel.ObjectState = ObjectState.Unchanged;

                fishCatchViewModel.FisherfolkId = fisherfolk.FisherfolkId;

                fisherfolkViewModel.FishCatches.Add(fishCatchViewModel);
            }

            return fisherfolkViewModel;
        }

        public static Fisherfolk CreateFisherfolkFromFisherfolkViewModel(FisherfolkViewModel fisherfolkViewModel)
        {
            Fisherfolk fisherfolk = new Fisherfolk();
            fisherfolk.FisherfolkId = fisherfolkViewModel.FisherfolkId;
            fisherfolk.FisherId = fisherfolkViewModel.FisherId;
            fisherfolk.LastName = fisherfolkViewModel.LastName;
            fisherfolk.ObjectState = fisherfolkViewModel.ObjectState;

            int tempfishCatchId = -1;

            foreach (FishCatchViewModel fishCatchViewModel in fisherfolkViewModel.FishCatches)
            {
                FishCatch fishCatch = new FishCatch();
                //fishCatch.CatchDate = fishCatchViewModel.CatchDate;
                fishCatch.FishName = fishCatchViewModel.FishName;
                fishCatch.Qty = fishCatchViewModel.Qty;
                fishCatch.UnitPrice = fishCatchViewModel.UnitPrice;
                fishCatch.ObjectState = fishCatchViewModel.ObjectState;

                if (fishCatchViewModel.ObjectState != ObjectState.Added)
                    fishCatch.FishCatchId = fishCatchViewModel.FishCatchId;
                else
                {
                    fishCatch.FishCatchId = tempfishCatchId;
                    tempfishCatchId--;
                }
                    
                fishCatch.FisherfolkId = fisherfolkViewModel.FisherfolkId;

                fisherfolk.FishCatches.Add(fishCatch);
            }

            return fisherfolk;
        }

        public static string GetMessageToUser(ObjectState objectState, string lastName)
        {
            string messageToUser = string.Empty;
            switch (objectState)
            {
                case ObjectState.Added:
                    messageToUser= string.Format("{0}'s Information successfully posted.", lastName);
                    break;
                case ObjectState.Modified:
                    messageToUser = string.Format("{0}'s Edited Information successfully posted.", lastName);
                    break;
            }

            return messageToUser;
        }
    }
}