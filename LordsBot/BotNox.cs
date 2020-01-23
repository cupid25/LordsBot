using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using System.Drawing;
using System.Threading;
using System.ComponentModel;


namespace LordsBot
{
    public class BotNox
    {
        public string name;
        public int ID;


        private static readonly Random getrandom = new Random();
        private static readonly object syncLock = new object();
        int randomNum;

        public int sleepTime;


        public bool alwaysShield;

        public bool shelterAll;
        public bool shelterOne;


        public bool collectChests;
        public bool collectGuildGift;
        public bool healTroops;
        public bool helpOthers;

        public bool Quest;
        public bool turfQuest;


        public bool trainTroops;
        public bool trainTroops_T1_Infantry;
        public bool trainTroops_T1_Ranged;
        public bool trainTroops_T1_Cavalry;
        public bool trainTroops_T1_Siege;

        public bool research;
        public bool upgradeBuildings;

        public bool gather;
        public bool foodGather;
        public bool oreGather;
        public bool woodGather;
        public bool stoneGather;
        public bool goldGather;

        private bool initialGather;
        public bool randomGather;
        public bool moveUpGather;
        public bool moveDownGather;
        public bool moveLeftGather;
        public bool moveRightGather;
        public int searchDistance;
        public int marchesGather;

        public bool useGuildBookmarks;
        public bool transferResource;
        public bool foodTransfer;
        public bool stoneTransfer;
        public bool woodTransfer;
        public bool oreTransfer;
        public bool goldTransfer;
        public bool transfer_FreqCheck = true;
        public int transfer_FreqCount = 1;
        public int account_Count = 1;
        public int transferFrequency;
        public int bookmarkNumberTransfer;
        public int marchesTransferFood;
        public int marchesTransferStone;
        public int marchesTransferWood;
        public int marchesTransferOre;
        public int marchesTransferGold;

        public bool AccountSwitch;
        public int AccountSwitch_Tracker;
        public int AccountSwitch_Tracker_Prev;
        public int AccountSwitch_Count;

        public int handler;
        private const double threshold = 0.8;

        public bool run = true;
        public bool True_Run = true;
        public bool correctWindowSizeEnable;
        public bool StartAppEnable = false;

        private int window_Width;
        private int window_Height;

        private int delay; // slow down searching if set on low performance

        private bool armyLimitReached = false;
        private Point point;

        BackgroundWorker worker;

        public BotNox()
        {

        }
        public BotNox(int handl)
        {
            handler = handl;
            delay = 3000;
            ID = 0;
            AccountSwitch_Tracker = 1;
            bookmarkNumberTransfer = 1;
            AccountSwitch_Count = 1;
            transfer_FreqCheck = true;
            transfer_FreqCount = 1;
            account_Count = 1;
            setWindowDimensions();
        }

        public void Start(BackgroundWorker b) {


            worker = b;


            worker.ReportProgress(ID, "Starting Up Bot...");
            if (correctWindowSizeEnable) {
                correctWindowSize();
            }

            

            while (True_Run) {
                while (run) {

                    StartApp();

                    if (run)
                    {

                        escPopUpAll_BruteForce();
                    }

                    if (run)
                    {
                        Go_To_Turf();
                    }

                    if (run)
                    {
                        if (shelterAll || shelterOne)
                        {
                            shelterTroops();
                        }
                    }


                    if (run)
                    {
                        if (alwaysShield)
                        {
                            Shield();
                        }

                    }


                    if (run)
                    {
                        if (collectChests)
                        {
                            mysteryBox();
                        }
                    }

                    if (run)
                    {
                        unhideUI();
                    }


                    if (run)
                    {
                        if (collectGuildGift)
                        {
                            guildGift();
                        }
                    }


                    if (run)
                    {
                        if (Quest || turfQuest)
                        {
                            DoQuest();
                        }
                    }


                    if (run)
                    {
                        if (helpOthers)
                        {
                            ClickHelps();
                        }
                    }

                    if (run)
                    {
                        if (healTroops)
                        {
                            HealTroops();
                        }
                    }


                    if (run)
                    {
                        if (trainTroops)
                        {
                            TrainTroops();
                        }
                    }

                    if (run)
                    {
                        if (upgradeBuildings)
                        {

                            BuildingUpGrade();

                        }
                    }


                    if (run)
                    {
                        if (research)
                        {
                            AutoResearch();
                        }
                    }


                    if (run)
                    {
                        if (transferResource || gather)
                        {
                            Go_To_KingdomMap();
                        }
                    }

                    if (run)
                    {
                                                   
                            if (transferResource)
                            {
                                
                                    Transfer_Resource();
                                
                            }
                        
                    }


                    if (run)
                    {
                        if (gather)
                        {
                            Gathering();
                        }
                    }




                    if (run)
                    {
                        Thread.Sleep(sleepTime * 1000);
                    }

                    if (AccountSwitch && run) {
                        Account_Switch();
                    }

                    worker.ReportProgress(ID, "Waiting...");
                    Thread.Sleep(sleepTime);
                }
                worker.ReportProgress(ID, "Bot has been stopped.");
                Thread.Sleep(20000);
            }



         

            

        }


        private bool StartApp()
        {

            const double threshold = 0.8;

            worker.ReportProgress(ID, "Starting Up Lords Mobile...");

            for (int timer1 = 0; timer1 <= 5; timer1++)
            {
                point = ImageProcessing.LocateImageSingle(handler, LordsBot.Properties.Resources.AppStart, threshold);
                if (point.X != 0 && point.Y != 0)
                {
                    //if app found click on it
                    MouseAction.SendMouseClick(handler, point.X, point.Y);

                    Thread.Sleep(20000);
                    MaintenanceLogIn();

                    for (int timer2 = 0; timer2 <= 5; timer2++)
                    {

                        //waits 5 seconds and attempt to exit out of popup
                        //tries to do it 5 times.

                        Thread.Sleep(10000);
                        
                        if (escPopUpAll_BruteForce() == true)
                        {
                            return true;
                        }

                    }

                    //This block of code executes if escPopUp fails

                    //Check if already in game.
                    if (isInGame())
                    {
                        worker.ReportProgress(ID, "Already in game...");
                        //is already in game
                        return true;
                    }
                    else
                    {
                        worker.ReportProgress(ID, "Failed to start game..");
                        //not in game, can't exit out of popup
                        return false;
                    }


                }
                else {

                    //this block of code execute if can't find app
                    worker.ReportProgress(ID, "Lords mobile app not found..");
                    //check if already in game
                    if (isInGame() || escPopUp())
                    {
                        worker.ReportProgress(ID, "Already in game..");
                        return true;
                    }
 

                }

                worker.ReportProgress(ID, "Attempting to start app again..");
                //attempts to click app again
                Thread.Sleep(5000);

                for (int k = 0; k < 4; k ++) {
                    hitEscape();
                    Thread.Sleep(2000);
                }


            }

            //fails to start app and not in game.
            return false;


         
         
        }
        private bool MaintenanceLogIn()
        {



            point = ImageProcessing.LocateImageSingle(handler, LordsBot.Properties.Resources.MaintenanceLogIn, 0.8);
            if (point.X != 0 && point.Y != 0)
            {
                MouseAction.SendMouseClick(handler, point.X, point.Y);
                Thread.Sleep(delay);

                return true;
            }
            return false;

        }

        private bool escPopUpAll() {

            int counter = 0;

            while (escPopUp())
            {
                counter++;
                if (counter >= 10) {
                    //check for error
                    return false;
                }

            }


            //return true when no more exit button to press
            return true;

        }
        private bool escPopUp() {

            Point point;
        

            point = ImageProcessing.LocateImageSingle(handler, LordsBot.Properties.Resources.Exit1, threshold);
            if (point.X != 0 && point.Y != 0)
            {
                MouseAction.SendMouseClick(handler, point.X, point.Y);
                Thread.Sleep(delay);
                return true;
            }
            
      

  
            point = ImageProcessing.LocateImageSingle(handler, LordsBot.Properties.Resources.ExitSpecial, threshold);
            if (point.X != 0 && point.Y != 0)
            {
                MouseAction.SendMouseClick(handler, point.X, point.Y);
                Thread.Sleep(delay);
                return true;

            }

                
            point = ImageProcessing.LocateImageSingle(handler, LordsBot.Properties.Resources.ExitSpecial2, threshold);
            if (point.X != 0 && point.Y != 0)
            {
                 MouseAction.SendMouseClick(handler, point.X, point.Y);
                 Thread.Sleep(delay);
                 return true;
            
            }
                    

             point = ImageProcessing.LocateImageSingle(handler, LordsBot.Properties.Resources.InfirmaryExitNotEnoughResource, threshold);

             if (point.X != 0 && point.Y != 0)
             {

                   MouseAction.SendMouseClick(handler, point.X, point.Y);
                   Thread.Sleep(delay);
                   return true;
            
             }

            point = ImageProcessing.LocateImageSingle(handler, LordsBot.Properties.Resources.InfirmaryExit1, threshold);
            if (point.X != 0 && point.Y != 0)
            {

                MouseAction.SendMouseClick(handler, point.X, point.Y);
                Thread.Sleep(delay);
                return true;

            }

            point = ImageProcessing.LocateImageSingle(handler, LordsBot.Properties.Resources.ExitSpecial3, threshold);
            if (point.X != 0 && point.Y != 0)
            {

                MouseAction.SendMouseClick(handler, point.X, point.Y);
                Thread.Sleep(delay);
                return true;

            }



            return false;
        }


        private bool escPopUpAll_BruteForce()
        {

            int counter = 0;

            while (escPopUp_BruteForce())
            {
                counter++;
                if (counter >= 10)
                {
                    //check for error
                    return false;
                }

            }


            //return true when no more exit button to press
            return true;

        }
        private bool escPopUp_BruteForce()
        {
            Point point;
            bool tempChecker0 = false;
            bool tempChecker1 = false;
            bool tempChecker2 = false;
            bool tempChecker3 = false;
            bool tempChecker4 = false;
            bool tempChecker5 = false;


            point = ImageProcessing.LocateImageSingle(handler, LordsBot.Properties.Resources.LevelupExit, threshold);
            if (point.X != 0 && point.Y != 0)
            {

                MouseAction.SendMouseClick(handler, point.X, point.Y);
                Thread.Sleep(delay);
                tempChecker0 = true;

            }

            point = ImageProcessing.LocateImageSingle(handler, LordsBot.Properties.Resources.InfirmaryExit1, threshold);
            if (point.X != 0 && point.Y != 0)
            {

                MouseAction.SendMouseClick(handler, point.X, point.Y);
                Thread.Sleep(delay);
                tempChecker1 = true;

            }


            point = ImageProcessing.LocateImageSingle(handler, LordsBot.Properties.Resources.Exit1, threshold);
            if (point.X != 0 && point.Y != 0)
            {
                MouseAction.SendMouseClick(handler, point.X, point.Y);
                Thread.Sleep(delay);
                tempChecker2 = true;
            }




            point = ImageProcessing.LocateImageSingle(handler, LordsBot.Properties.Resources.ExitSpecial, threshold);
            if (point.X != 0 && point.Y != 0)
            {
                MouseAction.SendMouseClick(handler, point.X, point.Y);
                Thread.Sleep(delay);
                tempChecker3 = true;

            }


            point = ImageProcessing.LocateImageSingle(handler, LordsBot.Properties.Resources.ExitSpecial2, threshold);
            if (point.X != 0 && point.Y != 0)
            {
                MouseAction.SendMouseClick(handler, point.X, point.Y);
                Thread.Sleep(delay);
                tempChecker4 = true;

            }


            point = ImageProcessing.LocateImageSingle(handler, LordsBot.Properties.Resources.InfirmaryExitNotEnoughResource, threshold);

            if (point.X != 0 && point.Y != 0)
            {

                MouseAction.SendMouseClick(handler, point.X, point.Y);
                Thread.Sleep(delay);
                tempChecker5 = true;

            }

            point = ImageProcessing.LocateImageSingle(handler, LordsBot.Properties.Resources.ExitSpecial3, threshold);

            if (point.X != 0 && point.Y != 0)
            {

                MouseAction.SendMouseClick(handler, point.X, point.Y);
                Thread.Sleep(delay);
                tempChecker5 = true;

            }

            if (tempChecker1 || tempChecker2 || tempChecker3 || tempChecker4 || tempChecker5 || tempChecker0) {
                return true;
            }



            return false;
        }

    

        private bool shelterTroops() {
           


            worker.ReportProgress(ID, "Starting: Shelter Troops");

            for (int i = 0; i < 2; i ++) {

                if (findShelter() == false) {
                    Go_To_Turf();
                    escPopUpAll_BruteForce();

                    findShelter();
                }

              

                //if not sheltered, do sheltering
                point = ImageProcessing.LocateImageSingle(handler, LordsBot.Properties.Resources.Shetler2, threshold);
                if (point.X != 0 && point.Y != 0)
                {
                    worker.ReportProgress(ID, "Sheltering Troops.");


                    if (shelterAction_Select12hour() == false) {
                        return false;
                    }
                    if (shelterOne == true) {
                        shelterActionOneTroop();
                    }
                    if (shelterAction_LaunchArmy() == false) {
                        return false;
                    }

                    return true;
                }



                //check if already sheltered and if need to be refreshed
                point = ImageProcessing.LocateImageSingle(handler, LordsBot.Properties.Resources.Shelter5, 0.8);//special threshold needed
                if (point.X != 0 && point.Y != 0)
                {

                    worker.ReportProgress(ID, "Already Sheltered.\nRefreshing Shelter Timer.");

                    shelterAction_Recall();
                    findShelter();
                    if (shelterAction_Select12hour() == false)
                    {
                        return false;
                    }
                    if (shelterOne == true)
                    {
                        shelterActionOneTroop();
                    }
                    if (shelterAction_LaunchArmy() == false)
                    {
                        return false;
                    }
                    return true;
                }

                //check if already sheltered
                point = ImageProcessing.LocateImageSingle(handler, LordsBot.Properties.Resources.Recall, threshold);
                if (point.X != 0 && point.Y != 0)
                {
                    worker.ReportProgress(ID, "Already Sheltered.");
                    escPopUpAll_BruteForce();
                    return true;
                }


                escPopUpAll_BruteForce();
                worker.ReportProgress(ID, "Sheltering failed, retrying.");
            }
            escPopUpAll_BruteForce();
            return false;

        }
        private bool findShelter() {
            //find shelter and click on it

            worker.ReportProgress(ID, "Searching for shelter.");

            //attempts to find 2 times


            hideUI();

                if (ShelterFound()) { return true; }

                //search up
                MouseAction.SendMouseDragUp(handler, window_Width, window_Height);
                Thread.Sleep(delay);
                if (ShelterFound()) { return true; }

                 for (int j = 0; j < 12; j++) {
                    //search right 5 times
                    MouseAction.SendMouseDragRightSmall(handler, window_Width, window_Height, 250);
                    Thread.Sleep(delay);

                 if (ShelterFound()) { return true; }


                    point = ImageProcessing.LocateImageSingle(handler, LordsBot.Properties.Resources.TopRightTurf, threshold);
                    if (point.X != 0 && point.Y != 0)
                    {
                        //check if at topRight of Turf
                        //Move onto searching Left if we are at top right.
                        break;
                    }

                }

                for (int k = 0; k < 4; k++)
                {
                    //search left 4 times
                    MouseAction.SendMouseDragLeftSmall(handler, window_Width, window_Height, 250);
                    Thread.Sleep(delay);

                if (ShelterFound()) { return true; }

                }

            

            worker.ReportProgress(ID, "Failed to find shelter.");
            //Fail to find shelter
            return false;

        }
        private bool ShelterFound(){

            const double threshold_TEMP = 0.7;

            point = ImageProcessing.LocateImageSingle(handler, LordsBot.Properties.Resources.Shelter, threshold_TEMP);
            if (point.X != 0 && point.Y != 0)
            {
                worker.ReportProgress(ID, "Shelter Found.");
                MouseAction.SendMouseClick(handler, point.X, point.Y);
                Thread.Sleep(delay);
                return true;
            }
            return false;
        }
        private bool shelterAction_Select12hour() {

          

                //check if 12 hour selected
            point = ImageProcessing.LocateImageSingle(handler, LordsBot.Properties.Resources.Shetler3, threshold);
            if (point.X != 0 && point.Y != 0)
            {
                MouseAction.SendMouseClick(handler, point.X, point.Y);
                    Thread.Sleep(delay/2);
                    //hit ok
                    point = ImageProcessing.LocateImageSingle(handler, LordsBot.Properties.Resources.Shetler2, threshold);
                    if (point.X != 0 && point.Y != 0)
                    {
                        MouseAction.SendMouseClick(handler, point.X, point.Y);
                        Thread.Sleep(delay/2);
                        return true;


                    }

                }
            else {
                    //check if 12 hour selected
                    point = ImageProcessing.LocateImageSingle(handler, LordsBot.Properties.Resources.Shetler4, threshold);
                    if (point.X != 0 && point.Y != 0)
                    {
                        //hit ok
                        point = ImageProcessing.LocateImageSingle(handler, LordsBot.Properties.Resources.Shetler2, threshold);
                        if (point.X != 0 && point.Y != 0)
                        {
                            MouseAction.SendMouseClick(handler, point.X, point.Y);
                            Thread.Sleep(delay/2);
                            return true;


                        }

                    }

                }




            return false;
        }
        private bool shelterAction_LaunchArmy() {
            //shelter action launch army
             point = ImageProcessing.LocateImageSingle(handler, LordsBot.Properties.Resources.LaunchArmy, threshold);
            if (point.X != 0 && point.Y != 0)
            {
                MouseAction.SendMouseClick(handler, point.X, point.Y);
                Thread.Sleep(delay);
                //if fail to launch, no troops to shelter, exit out.
                //check if still in launch page
                point = ImageProcessing.LocateImageSingle(handler, LordsBot.Properties.Resources.LaunchArmy, threshold);
                if (point.X != 0 && point.Y != 0)
                {
                    //needs to exit twice.
                    escPopUpAll();
                }

                    return true;
            }

            return false;
        }
        private bool shelterAction_Recall() {
            //Recall troops from shelter
            
            point = ImageProcessing.LocateImageSingle(handler, LordsBot.Properties.Resources.Recall, threshold);
            if (point.X != 0 && point.Y != 0)
            {
                MouseAction.SendMouseClick(handler, point.X, point.Y);
                worker.ReportProgress(ID, "Recalling troops from shelter.");
                Thread.Sleep(delay/2);
                return true;
            }

            return false;
        }
        private bool shelterActionOneTroop() {
           

            point = ImageProcessing.LocateImageSingle(handler, LordsBot.Properties.Resources.Shelter_OneTroopClear, threshold);
            if (point.X != 0 && point.Y != 0)
            {
                MouseAction.SendMouseClick(handler, point.X, point.Y);
                Thread.Sleep(delay/2);
                
            }

            point = ImageProcessing.LocateImageSingle(handler, LordsBot.Properties.Resources.Shelter_OneTroop, threshold);
            if (point.X != 0 && point.Y != 0)
            {
                MouseAction.SendMouseClick(handler, point.X, point.Y);
                Thread.Sleep(delay/2);
                return true;
            }

            return false;
        }




        private bool Shield() {


            worker.ReportProgress(ID, "Checking Shield..");
            for (int k = 0; k < 2; k++) {
                point = ImageProcessing.LocateImageSingle(handler, LordsBot.Properties.Resources.shield3, threshold);
                if (point.X != 0 && point.Y != 0)
                {
                    worker.ReportProgress(ID, "Already shielded...");
                    return true;
                }

                for (int i = 0; i < 2; i++) {
                    point = ImageProcessing.LocateImageSingle(handler, LordsBot.Properties.Resources.shield1, threshold);
                    if (point.X != 0 && point.Y != 0)
                    {
                        MouseAction.SendMouseClick(handler, point.X, point.Y);
                        Thread.Sleep(delay);
                        if (ShieldAction())
                        {
                            return true;
                        }
                    }
                    point = ImageProcessing.LocateImageSingle(handler, LordsBot.Properties.Resources.shield2, threshold);
                    if (point.X != 0 && point.Y != 0)
                    {
                        MouseAction.SendMouseClick(handler, point.X, point.Y);
                        Thread.Sleep(delay);
                        if (ShieldAction())
                        {
                            return true;
                        }
                    }
                    point = ImageProcessing.LocateImageSingle(handler, LordsBot.Properties.Resources.shield, threshold);
                    if (point.X != 0 && point.Y != 0)
                    {
                        MouseAction.SendMouseClick(handler, point.X, point.Y);
                        Thread.Sleep(delay);
                        if (ShieldAction())
                        {
                            return true;
                        }
                    }
                }

                escPopUpAll_BruteForce();
                //failed to find shield status, attempting to exit out of any popups and retry.
            }
            escPopUpAll_BruteForce();
            return false;
        }
        private bool ShieldAction()
        {


            worker.ReportProgress(ID, "Attempting to shield...");

            point = ImageProcessing.LocateImageSingle(handler, LordsBot.Properties.Resources.shield4, threshold);
            if (point.X != 0 && point.Y != 0)
            {
                MouseAction.SendMouseClick(handler, point.X, point.Y);
                Thread.Sleep(delay);

                //use shield
                point = ImageProcessing.LocateImageSingle(handler, LordsBot.Properties.Resources.use, threshold);
                if (point.X != 0 && point.Y != 0)
                {
                    MouseAction.SendMouseClick(handler, point.X, point.Y);

                    worker.ReportProgress(ID, "Successfully shielded.");

                    Thread.Sleep(delay);
                    escPopUp();

                    return true;
                }
            }

            worker.ReportProgress(ID, "Failed to shield.");
            return false;
        }


        private bool mysteryBox() {

            worker.ReportProgress(ID, "Checking mystery box.");
            const double threshold_TEMP = 0.7;

            mysteryBox_ManualClick();
            if (mysteryBoxCollect())
            {
                return true;
            }

            for (int k = 0; k < 2; k++) {
                point = ImageProcessing.LocateImageSingle(handler, LordsBot.Properties.Resources.mysterybox4, threshold_TEMP);
                if (point.X != 0 && point.Y != 0)
                {

                    MouseAction.SendMouseClick(handler, point.X, point.Y);
                    Thread.Sleep(delay);

                    if (mysteryBoxCollect())
                    {
                        return true;
                    }
                }


                for (int i = 0; i < 5; i++) { // tries 5 times

                    point = ImageProcessing.LocateImageSingle(handler, LordsBot.Properties.Resources.mysterybox, threshold_TEMP);
                    if (point.X == 0 && point.Y == 0)
                    {
                        MouseAction.SendMouseClick(handler, point.X, point.Y);
                        Thread.Sleep(delay);

                        if (mysteryBoxCollect()) {
                            return true;
                        }
                    }

                    point = ImageProcessing.LocateImageSingle(handler, LordsBot.Properties.Resources.mysterybox5, threshold_TEMP);
                    if (point.X != 0 && point.Y != 0)
                    {
                        MouseAction.SendMouseClick(handler, point.X, point.Y);
                        Thread.Sleep(delay);

                        if (mysteryBoxCollect())
                        {
                            return true;
                        }

                    }


                }

                escPopUpAll_BruteForce();
                //failed, attempting to exit out of any popups and retry.

            }
            escPopUpAll_BruteForce();
            worker.ReportProgress(ID, "Failed to find mystery box");
            return false;
        }
        private void mysteryBox_ManualClick() {
            MouseAction.SendMouseClick(handler, 793, 392);
            Thread.Sleep(delay);
           
        }
        private bool mysteryBoxCollect()
        {


           
                MouseAction.SendMouseClick(handler, 464, 383);



                Thread.Sleep(delay);


                return true;
            
        }




        private bool guildGift() {

            for (int i = 0; i < 2; i++) {

                worker.ReportProgress(ID, "Checking guild gifts");

                if (openGuildTab() == true) {

                    if (openGuildGiftTab() == true)
                    {
                        if (openGuildGifts() == true)
                        {
                            escPopUpAll();
                            return true;
                        }
                    }
                }

                escPopUpAll_BruteForce();
                UnHideUI_Map();
                unhideUI();
            }


            escPopUpAll_BruteForce();

            return false;
        }
        private bool openGuildTab() {


            worker.ReportProgress(ID, "Opening guild tab.");

            for (int i = 0; i <= 15; i++) {
                point = ImageProcessing.LocateImageSingle(handler, LordsBot.Properties.Resources.guild1, threshold);
                if (point.X != 0 && point.Y != 0)
                {
                    MouseAction.SendMouseClick(handler, point.X, point.Y);
                    Thread.Sleep(delay);
                    return true;
                }
                point = ImageProcessing.LocateImageSingle(handler, LordsBot.Properties.Resources.guild2, threshold);
                if (point.X != 0 && point.Y != 0)
                {
                    MouseAction.SendMouseClick(handler, point.X, point.Y);
                    Thread.Sleep(delay);
                    return true;
                }


            }


            return false;
        }
        private bool openGuildGiftTab()
        {

            point = ImageProcessing.LocateImageSingle(handler, LordsBot.Properties.Resources.guildgift1, threshold);
            if (point.X != 0 && point.Y != 0)
            {
                MouseAction.SendMouseClick(handler, point.X, point.Y);
                Thread.Sleep(delay);
                return true;
            }

            return false;
        }
        private bool openGuildGifts()
        {



            point = ImageProcessing.LocateImageSingle(handler, LordsBot.Properties.Resources.NoGuildGifts, threshold);
            if (point.X != 0 && point.Y != 0)
            {

                worker.ReportProgress(ID, "No guild gifts to collect.");
                //return true if no more gift to open, or delete
                return true;
            }


            point = ImageProcessing.LocateImageSingle(handler, LordsBot.Properties.Resources.guildgiftOpenAll, threshold);
            if (point.X != 0 && point.Y != 0)
            {
                MouseAction.SendMouseClick(handler, point.X, point.Y);
                Thread.Sleep(delay);

                point = ImageProcessing.LocateImageSingle(handler, LordsBot.Properties.Resources.guildgiftDeleteAll, threshold);
                if (point.X != 0 && point.Y != 0)
                {
                    MouseAction.SendMouseClick(handler, point.X, point.Y);
                    Thread.Sleep(delay);

                    point = ImageProcessing.LocateImageSingle(handler, LordsBot.Properties.Resources.guildgiftOpen, threshold);
                    if (point.X == 0 && point.Y == 0)
                    {

                        point = ImageProcessing.LocateImageSingle(handler, LordsBot.Properties.Resources.guildgiftDelete, threshold);
                        if (point.X == 0 && point.Y == 0)
                        {

                            worker.ReportProgress(ID, "Successfully collected guild gifts.");
                            //return true if no more gift to open, or delete
                            return true;
                        }

                       
                    }

                  
                }

            }


            bool tempRun = true;
            bool tempRun2 = true;
            bool tempRun3 = true;
            while (tempRun)
            {
                while (tempRun2) {
                    //open guild gifts
                    point = ImageProcessing.LocateImageSingle(handler, LordsBot.Properties.Resources.guildgiftOpen, threshold);
                    if (point.X != 0 && point.Y != 0)
                    {
                        MouseAction.SendMouseClick(handler, point.X, point.Y);
                        Thread.Sleep(delay);

                    }
                    else {
                        //stop opening if no more to open
                        tempRun2 = false;
                    }
                }

                //Press delete all button
                point = ImageProcessing.LocateImageSingle(handler, LordsBot.Properties.Resources.guildgiftDeleteAll, threshold);
                if (point.X != 0 && point.Y != 0)
                {
                    MouseAction.SendMouseClick(handler, point.X, point.Y);
                    Thread.Sleep(delay);

                }

                while (tempRun3)
                {
                    //delete opened chests one by one
                    point = ImageProcessing.LocateImageSingle(handler, LordsBot.Properties.Resources.guildgiftDelete, threshold);
                    if (point.X != 0 && point.Y != 0)
                    {
                        MouseAction.SendMouseClick(handler, point.X, point.Y);
                        Thread.Sleep(delay);

                    }
                    else
                    {
                        //stop deleteing if no more to delete
                        tempRun3 = false;
                    }
                }


                //check if there is more to open
                point = ImageProcessing.LocateImageSingle(handler, LordsBot.Properties.Resources.guildgiftOpen, threshold);
                if (point.X != 0 && point.Y != 0)
                {
                    //if more to open, start back to top of loop.
                    tempRun2 = true;
                    tempRun3 = true;

                }
                else
                {
                    worker.ReportProgress(ID, "Successfully collected guild gifts.");

                    //stop opening and deleteing if all is done
                    return true;
                }

            }
            

            return false;
        }



        private bool HealTroops() {

            worker.ReportProgress(ID, "Checking infirmary.");

            if (InfirmarySearch()) {
                if (InfirmaryHealTroop()) {
                    escPopUpAll_BruteForce();
                    return true;
                }
            }


            escPopUpAll_BruteForce();
            return false;
        }
        private bool InfirmarySearch() {


            worker.ReportProgress(ID, "Searching for troops to heal.");

            hideUI();

            if (InfirmaryOpen()) { return true; }
            MouseAction.SendMouseDragUp(handler, window_Width, window_Height);
            Thread.Sleep(delay);
            if (Is_In_Turf() == false) {
                escPopUpAll_BruteForce();
            }

            if (InfirmaryOpen()) { return true; }

            for (int j = 0; j <= 4; j++)
            {
                //search right 4 times
                MouseAction.SendMouseDragRight(handler, window_Width, window_Height);
                Thread.Sleep(delay);
                if (Is_In_Turf() == false)
                {
                    escPopUpAll_BruteForce();
                }


                if (InfirmaryOpen()) { return true; }

                point = ImageProcessing.LocateImageSingle(handler, LordsBot.Properties.Resources.TopRightTurf, threshold);
                if (point.X != 0 && point.Y != 0)
                {
                    //check if at topRight of Turf               
                    break;
                }

            }

            MouseAction.SendMouseDragDownSmall(handler, window_Width, window_Height, 120);
            Thread.Sleep(delay);
            if (Is_In_Turf() == false)
            {
                escPopUpAll_BruteForce();
            }


            if (InfirmaryOpen()) { return true; }

            for (int k = 0; k <= 1; k++)
            {
                //search left 2 times
                MouseAction.SendMouseDragLeft(handler, window_Width, window_Height);
                Thread.Sleep(delay);
                if (Is_In_Turf() == false)
                {
                    escPopUpAll_BruteForce();
                }


                if (InfirmaryOpen()) { return true; }

            }

            

            return false;
        }
        private bool InfirmaryOpen()
        {




           
                point = ImageProcessing.LocateImageSingle(handler, LordsBot.Properties.Resources.heal1, threshold);
                if (point.X != 0 && point.Y != 0)
                {
                    MouseAction.SendMouseClick(handler, point.X, point.Y);
                    Thread.Sleep(delay);
                    return true;
                }
                point = ImageProcessing.LocateImageSingle(handler, LordsBot.Properties.Resources.heal2, threshold);
                if (point.X != 0 && point.Y != 0)
                {
                    MouseAction.SendMouseClick(handler, point.X, point.Y);
                    Thread.Sleep(delay);
                    return true;
                }
                point = ImageProcessing.LocateImageSingle(handler, LordsBot.Properties.Resources.heal3, threshold);
                if (point.X != 0 && point.Y != 0)
                {
                    MouseAction.SendMouseClick(handler, point.X, point.Y);
                    Thread.Sleep(delay);
                    return true;
                }
                point = ImageProcessing.LocateImageSingle(handler, LordsBot.Properties.Resources.heal4, threshold);
                if (point.X != 0 && point.Y != 0)
                {
                    MouseAction.SendMouseClick(handler, point.X, point.Y);
                    Thread.Sleep(delay);
                    return true;
                }
            

            return false;
        }
        private bool InfirmaryHealTroop()
        {


            point = ImageProcessing.LocateImageSingle(handler, LordsBot.Properties.Resources.healButton, threshold);
            if (point.X != 0 && point.Y != 0)
            {
                    MouseAction.SendMouseClick(handler, point.X, point.Y);
                    worker.ReportProgress(ID, "Healing troops.");
                    Thread.Sleep(delay);
                    
                return true;
            }

            point = ImageProcessing.LocateImageSingle(handler, LordsBot.Properties.Resources.healButtonRed, threshold);
            if (point.X != 0 && point.Y != 0)
            {

                worker.ReportProgress(ID, "Not enough resourch to heal.");

                return false;
            }

            return false;
        }
      


        private bool ClickHelps() {

            worker.ReportProgress(ID, "Checking helps.");

            if (OpenHelpTab()) {
               PressHelps();

                escPopUpAll_BruteForce();
            }


            return false;
        }
        private bool OpenHelpTab()
        {

            const double threshold_TEMP = 0.8;

            point = ImageProcessing.LocateImageSingle(handler, LordsBot.Properties.Resources.helpButton2, threshold_TEMP);
            if (point.X != 0 && point.Y != 0)
            {
                MouseAction.SendMouseClick(handler, point.X, point.Y);
                Thread.Sleep(delay);
                return true;
            }
            point = ImageProcessing.LocateImageSingle(handler, LordsBot.Properties.Resources.helpButton, threshold_TEMP);
            if (point.X != 0 && point.Y != 0)
            {
                MouseAction.SendMouseClick(handler, point.X, point.Y);
                Thread.Sleep(delay);
                return true;
            }


            return false;
        }
        private bool PressHelps()
        {

           
                MouseAction.SendMouseClick(handler, 425, 490);
                worker.ReportProgress(ID, "Successfully clicked helps.");
                Thread.Sleep(delay);
                return true;
            


           
        }



        private bool DoQuest() {

            worker.ReportProgress(ID, "Checking quests...");

            if (QuestTabOpen()) {

                if (turfQuest) {
                    TurfQuest();
                }

                if (Quest) {
                    Admin_Guild_VIP_QuestsCollect();
                }
                

                escPopUpAll_BruteForce();

                return true;
            }

            escPopUpAll_BruteForce();

            return false;
        }
        private bool QuestTabOpen() {

            unhideUI();

            worker.ReportProgress(ID, "Opening quest page.");

            MouseAction.SendMouseClick(handler, 572, 500);
            Thread.Sleep(delay);
        

            return true;

        }
        private bool TurfQuest() {

            worker.ReportProgress(ID, "Opening turf quests tab.");
            TurfQuestOpenTab();
            QuestCollect();

            return false;
        }
        private bool TurfQuestOpenTab() {



            MouseAction.SendMouseClick(handler, 167, 129);
            Thread.Sleep(delay);
            return true;
                
        }

        private bool Admin_Guild_VIP_QuestsCollect() {

          
            AdminQuestOpenTab();
            if (!CheckIf_OnGoing_Quest()) {
                Quests_Collect();
            }
  


            GuildQuestOpenTab();
            if (!CheckIf_OnGoing_Quest())
            {
                Quests_Collect();
            }


            VipQuestOpenTab();
            if (VipQuestCanClaim())
            {
                if (VipQuestCollect())
                {
                    worker.ReportProgress(ID, "Collected VIP chest.");
                }
            }
            else {
                worker.ReportProgress(ID, "No VIP chest to collect.");
            }

        

            return false;
        }
        private bool Quests_Collect() {


            int errorCounter = 0;

            if (QuestStart())
            {
        
                return true;
            }
            else {
                QuestCollect();
                    if (QuestStart())
                    {
                        return true;
                    }
                
            }

            while (!CheckIfNoNewQuest())
            {

                QuestComplete();

                errorCounter++;

                if (errorCounter >= 2)
                {
                    //report error
                    return false;
                }

            };
            return false;
        }     
        private bool AdminQuestOpenTab() {


            MouseAction.SendMouseClick(handler, 317, 129);

            Thread.Sleep(delay);
            return true;
        
        }
        private bool GuildQuestOpenTab() {

            MouseAction.SendMouseClick(handler, 485, 129);
            Thread.Sleep(delay);

            return true;               
        }
        private bool QuestCollect() {

            const double threshold_TEMP = 0.7;
            const int delay_TEMP = 2000;

            bool questCollected = false;

            worker.ReportProgress(ID, "Starting quest collection.");


            for (int i = 0; i < 2; i++) {
                do
                {
                    
                    point = ImageProcessing.LocateImageSingle(handler, LordsBot.Properties.Resources.Collect1, threshold_TEMP);
                    if (point.X != 0 && point.Y != 0)
                    {
                        MouseAction.SendMouseClick(handler, point.X, point.Y);
                        Thread.Sleep(delay_TEMP);
                        questCollected = true;
                    }
                    else
                    {
                        point = ImageProcessing.LocateImageSingle(handler, LordsBot.Properties.Resources.Collect2, threshold_TEMP);
                        if (point.X != 0 && point.Y != 0)
                        {
                            MouseAction.SendMouseClick(handler, point.X, point.Y);
                            Thread.Sleep(delay_TEMP);
                            questCollected = true;
                        }

                               else
                                {


                            questCollected = false;
                                        
                                    
                                }
                                                 

                    }

                    CheckLevelUp();
                } while (questCollected);
            }
            worker.ReportProgress(ID, "No more quest to collect.");
            //no more quest to collect
            return false;

        }
        private bool QuestComplete() {

           const double threshold_TEMP = 0.7;
            const int delay_TEMP = 2000;
            bool questCompleted = false;

            worker.ReportProgress(ID, "Starting quest completetion.");

            for (int i = 0; i < 2; i++)
            {
                do
                {
                    
                    point = ImageProcessing.LocateImageSingle(handler, LordsBot.Properties.Resources.AutoComplete1, threshold_TEMP);
                    if (point.X != 0 && point.Y != 0)
                    {
                        MouseAction.SendMouseClick(handler, point.X, point.Y);
                        Thread.Sleep(delay_TEMP);
                        questCompleted = true;
                    }
                    else
                    {
                        point = ImageProcessing.LocateImageSingle(handler, LordsBot.Properties.Resources.AutoComplete2, threshold_TEMP);
                        if (point.X != 0 && point.Y != 0)
                        {
                            MouseAction.SendMouseClick(handler, point.X, point.Y);
                            Thread.Sleep(delay_TEMP);
                            questCompleted = true;
                        }

                                        else
                                        {
                                            questCompleted = false;
                                        }

                                  
                                                                                

                    }

                    CheckLevelUp();

                    if (CheckIfNoNewQuest())
                    {
                        return true;
                    }




                } while (questCompleted);
            }

            //no more quest to complete
            return true;

        }
        private bool QuestStart() {

            const double threshold_TEMP = 0.7;

            point = ImageProcessing.LocateImageSingle(handler, LordsBot.Properties.Resources.QuestStart, threshold_TEMP);
                if (point.X != 0 && point.Y != 0)
                {
                    MouseAction.SendMouseClick(handler, point.X, point.Y);
                worker.ReportProgress(ID, "Starting new quest.");
                Thread.Sleep(delay);
                    return true;
                }
            
            return false;
        }
        private bool VipQuestOpenTab() {




            MouseAction.SendMouseClick(handler, 655, 129);
            Thread.Sleep(delay);
            return true;
             
        }
        private bool VipQuestCanClaim() {

           
           
                point = ImageProcessing.LocateImageSingle(handler, LordsBot.Properties.Resources.VipQuestClaim1, threshold);
                if (point.X != 0 && point.Y != 0)
                {

                    return true;
                }
                point = ImageProcessing.LocateImageSingle(handler, LordsBot.Properties.Resources.VipQuestClaim2, 0.7);
                if (point.X != 0 && point.Y != 0)
                {

                    return true;
                }
            
            return false;
        }
        private bool VipQuestCollect() {

            const double threshold_TEMP = 0.7;
            worker.ReportProgress(ID, "Starting VIP Box collection.");

            for (int i = 0; i < 3; i++)
            {

                point = ImageProcessing.LocateImageSingle(handler, LordsBot.Properties.Resources.VipQuestChest1, threshold_TEMP);
                if (point.X != 0 && point.Y != 0)
                {
                    MouseAction.SendMouseClick(handler, point.X, point.Y);
                    Thread.Sleep(delay);
                    return true;
                }

                point = ImageProcessing.LocateImageSingle(handler, LordsBot.Properties.Resources.VipQuestChest2, threshold_TEMP);
                if (point.X != 0 && point.Y != 0)
                {
                    MouseAction.SendMouseClick(handler, point.X, point.Y);
                    Thread.Sleep(delay);
                    return true;
                }
                point = ImageProcessing.LocateImageSingle(handler, LordsBot.Properties.Resources.VipQuestChest3, threshold_TEMP);
                if (point.X != 0 && point.Y != 0)
                {
                    MouseAction.SendMouseClick(handler, point.X, point.Y);
                    Thread.Sleep(delay);
                    return true;
                }
                point = ImageProcessing.LocateImageSingle(handler, LordsBot.Properties.Resources.VipQuestChest4, threshold_TEMP);
                if (point.X != 0 && point.Y != 0)
                {
                    MouseAction.SendMouseClick(handler, point.X, point.Y);
                    Thread.Sleep(delay);
                    return true;
                }
                point = ImageProcessing.LocateImageSingle(handler, LordsBot.Properties.Resources.VipQuestChest5, threshold_TEMP);
                if (point.X != 0 && point.Y != 0)
                {
                    MouseAction.SendMouseClick(handler, point.X, point.Y);
                    Thread.Sleep(delay);
                    return true;
                }

                point = ImageProcessing.LocateImageSingle(handler, LordsBot.Properties.Resources.VipQuestChest6, threshold_TEMP);
                if (point.X != 0 && point.Y != 0)
                {
                    MouseAction.SendMouseClick(handler, point.X, point.Y);
                    Thread.Sleep(delay);
                    return true;
                }




            }
       


            //no vip quest to collect
            return false;

        }

        private bool CheckIfNoNewQuest() {

         

            point = ImageProcessing.LocateImageSingle(handler, LordsBot.Properties.Resources.WaitForNewQuest, threshold);
            if (point.X != 0 && point.Y != 0)
            {
                return true;
            }
            return false;
        }
        private bool CheckIf_OnGoing_Quest() {
          

            point = ImageProcessing.LocateImageSingle(handler, LordsBot.Properties.Resources.Quest_OnGoing, threshold);
            if (point.X != 0 && point.Y != 0)
            {
                return true;
            }
            return false;
        }



        private bool TrainTroops() {


            worker.ReportProgress(ID, "Starting Troop Training.");


            if (SearchBarracks()) {

                if (trainTroops_T1_Infantry) {
                    Barracks_T1inf();
                }
                else if (trainTroops_T1_Ranged)
                {
                    Barracks_T1Archer();
                }
                else if (trainTroops_T1_Cavalry)
                {
                    Barracks_T1Cav();
                }
                else if (trainTroops_T1_Siege)
                {
                    Barracks_T1Siege();
                }


            }

           


            escPopUpAll_BruteForce();          
            return false;
        }
        private bool SearchBarracks() {

            worker.ReportProgress(ID, "Searching for barracks.");

            hideUI();

            
            if (BarracksOpen()) { return true; }
            MouseAction.SendMouseDragUp(handler, window_Width, window_Height);
            Thread.Sleep(delay);
            if (Is_In_Turf() == false)
            {
                escPopUpAll_BruteForce();
            }


            if (BarracksOpen()) { return true; }

            for (int j = 0; j <= 4; j++)
            {
                //search right 4 times
                MouseAction.SendMouseDragRight(handler, window_Width, window_Height);
                Thread.Sleep(delay);
                if (Is_In_Turf() == false)
                {
                    escPopUpAll_BruteForce();
                }


                if (BarracksOpen()) { return true; }

                point = ImageProcessing.LocateImageSingle(handler, LordsBot.Properties.Resources.TopRightTurf, threshold);
                if (point.X != 0 && point.Y != 0)
                {
                    //check if at topRight of Turf               
                    break;
                }

            }

            MouseAction.SendMouseDragDownSmall(handler, window_Width, window_Height, 120);
            Thread.Sleep(delay);
            if (Is_In_Turf() == false)
            {
                escPopUpAll_BruteForce();
            }

            if (BarracksOpen()) { return true; }

            for (int k = 0; k <= 1; k++)
            {
                //search left 2 times
                MouseAction.SendMouseDragLeft(handler, window_Width, window_Height);
                Thread.Sleep(delay);
                if (Is_In_Turf() == false)
                {
                    escPopUpAll_BruteForce();
                }


                if (BarracksOpen()) { return true; }

            }



            return false;
        }
        private bool BarracksOpen() {

            

            double threshold_TEMP = 0.7;

            for (int i = 0; i <= 2; i++) {
                point = ImageProcessing.LocateImageSingle(handler, LordsBot.Properties.Resources.Barracks_Idle, threshold_TEMP);
                if (point.X != 0 && point.Y != 0)
                {
                    MouseAction.SendMouseClick(handler, point.X, point.Y);
                    Thread.Sleep(delay);
                    return true;
                }
            }
            return false;
        }
        private bool Barracks_T1Siege() {

            

            point = ImageProcessing.LocateImageSingle(handler, LordsBot.Properties.Resources.Barracks_T1Siege, threshold);
            if (point.X != 0 && point.Y != 0)
            {
                MouseAction.SendMouseClick(handler, point.X, point.Y);
                Thread.Sleep(delay);

                Barracks_TrainButton();

                return true;
            }
            return false;

        }
        private bool Barracks_T1Archer()
        {


            point = ImageProcessing.LocateImageSingle(handler, LordsBot.Properties.Resources.Barracks_T1Archer, threshold);
            if (point.X != 0 && point.Y != 0)
            {
                MouseAction.SendMouseClick(handler, point.X, point.Y);
                Thread.Sleep(delay);

                Barracks_TrainButton();

                return true;
            }
            return false;

        }
        private bool Barracks_T1Cav()
        {


            point = ImageProcessing.LocateImageSingle(handler, LordsBot.Properties.Resources.Barracks_T1Cav, threshold);
            if (point.X != 0 && point.Y != 0)
            {
                MouseAction.SendMouseClick(handler, point.X, point.Y);
                Thread.Sleep(delay);

                Barracks_TrainButton();

                return true;
            }
            return false;

        }
        private bool Barracks_T1inf()
        {


            point = ImageProcessing.LocateImageSingle(handler, LordsBot.Properties.Resources.Barracks_T1inf, threshold);
            if (point.X != 0 && point.Y != 0)
            {
                MouseAction.SendMouseClick(handler, point.X, point.Y);
                Thread.Sleep(delay);

                Barracks_TrainButton();

                return true;
            }
            return false;

        }
        private bool Barracks_TrainButton()
        {
            

            point = ImageProcessing.LocateImageSingle(handler, LordsBot.Properties.Resources.Barracks_TrainButton, threshold);
            if (point.X != 0 && point.Y != 0)
            {
                MouseAction.SendMouseClick(handler, point.X, point.Y);
                Thread.Sleep(delay);
                return true;
            }
            return false;

        }



        private bool BuildingUpGrade() {

            worker.ReportProgress(ID, "Starting building upgrade.");

            if (PressFreeButton()) {
                PressFreeButton();
            }
            if (RequestHelp()) {
                RequestHelp();
            }

            do {

                if (BuildingUpGradeSearch() == false) {
                    break;
                }
               

                //Upgrade button needs to be press twice.
                BuildingUpGradeButton();
                BuildingUpGradeButton();

            } while (PressFreeButton());


            escPopUpAll_BruteForce();
            unhideUI();

            if (PressFreeButton())
            {
                PressFreeButton();
            }
            if (RequestHelp())
            {
                RequestHelp();
            }


            return false;
        }
        private bool BuildingUpGradeSearch2() {
            worker.ReportProgress(ID, "Searching for a building to upgrade.");
            hideUI();


            if (BuildingUpGradeFound()) { return true; }

            Turf_GoToButtomRight();
            for (int k = 0; k <= 2; k++) {

                for (int i = 0; i <= 3; i++)
                {
                    MouseAction.SendMouseDragLeft(handler, window_Width, window_Height);
                    Thread.Sleep(delay);
                    if (Is_In_Turf() == false)
                    {
                        escPopUpAll_BruteForce();
                    }
                    if (BuildingUpGradeFound()) { return true; }
                }

                MouseAction.SendMouseDragUpSmall_AccountSwitch(handler, window_Width, window_Height, 50);
                Thread.Sleep(delay);
                if (Is_In_Turf() == false)
                {
                    escPopUpAll_BruteForce();
                }
                if (BuildingUpGradeFound()) { return true; }

                for (int i = 0; i <= 3 ; i++)
                {
                    MouseAction.SendMouseDragRight(handler, window_Width, window_Height);
                    Thread.Sleep(delay);
                    if (Is_In_Turf() == false)
                    {
                        escPopUpAll_BruteForce();
                    }
                    if (BuildingUpGradeFound()) { return true; }
                }
                if (k < 2) {
                    MouseAction.SendMouseDragUpSmall_AccountSwitch(handler, window_Width, window_Height, 50);
                    Thread.Sleep(delay);
                    if (Is_In_Turf() == false)
                    {
                        escPopUpAll_BruteForce();
                    }
                }
                if (BuildingUpGradeFound()) { return true; }
            }

            return false;
        }
        private bool BuildingUpGradeSearch()
        {
            worker.ReportProgress(ID, "Searching for a building to upgrade.");
            hideUI();


            if (BuildingUpGradeFound()) { return true; }

            Turf_GoToButtomRight();
            for (int k = 0; k <= 3; k++)
            {

                for (int i = 0; i < 4; i++)
                {
                    MouseAction.SendMouseDragUpSmall_AccountSwitch(handler, window_Width, window_Height, 75);
                    Thread.Sleep(delay/2);
                    if (Is_In_Turf() == false)
                    {
                        escPopUpAll_BruteForce();
                    }
                    if (BuildingUpGradeFound()) { return true; }
                }
                for (int i = 0; i < 2; i++)
                {
                    MouseAction.SendMouseDragLeftSmall_AccountSwitch(handler, window_Width, window_Height, 120);
                    Thread.Sleep(delay / 2);
                    if (Is_In_Turf() == false)
                    {
                        escPopUpAll_BruteForce();
                    }
                    if (BuildingUpGradeFound()) { return true; }
                }
                for (int i = 0; i < 4; i++)
                {
                    MouseAction.SendMouseDragDownSmall_AccountSwitch(handler, window_Width, window_Height, 75);
                    Thread.Sleep(delay/2);
                    if (Is_In_Turf() == false)
                    {
                        escPopUpAll_BruteForce();
                    }
                    if (BuildingUpGradeFound()) { return true; }
                }
                if (k != 3) {
                    for (int i = 0; i < 2; i++)
                    {
                        MouseAction.SendMouseDragLeftSmall_AccountSwitch(handler, window_Width, window_Height, 120);
                        Thread.Sleep(delay / 2);
                        if (Is_In_Turf() == false)
                        {
                            escPopUpAll_BruteForce();
                        }

                        if (BuildingUpGradeFound()) { return true; }
                    }
                }
            }

            return false;
        }
        private bool BuildingUpGradeFound()
        {
          
            double threshold_TEMP = 0.95;

            point = ImageProcessing.LocateImageSingle(handler, LordsBot.Properties.Resources.UpgradeBuilding1, threshold_TEMP);
            if (point.X != 0 && point.Y != 0)
            {
                MouseAction.SendMouseClick(handler, point.X - 24, point.Y - 7);
                Thread.Sleep(delay);

                return true;
            }

            return false;
        }
        private bool BuildingUpGradeButton()
        {
            


            point = ImageProcessing.LocateImageSingle(handler, LordsBot.Properties.Resources.UpgradeBuildingButton1, threshold);
            if (point.X != 0 && point.Y != 0)
            {
                MouseAction.SendMouseClick(handler, point.X, point.Y);
                Thread.Sleep(delay);

                return true;
            }

            return false;
        }




        private bool AutoResearch() {

            int errorCounter = 0;

            worker.ReportProgress(ID, "Starting Auto Research.");

            do {
                if (Research_SearchForBuilding()) {
                    Research_EconomyTree();
                    Research_MilitaryTree();
                }

                errorCounter++;

                if (errorCounter >= 10) {
                    break;
                }

            } while (PressFreeButton());

            RequestHelp();
            escPopUpAll_BruteForce();

            return false;
        }
        private bool Research_SearchForBuilding1()
        {

            hideUI();


            if (Research_SearchForBuildingFound()) { return true; }

            Turf_GoToButtomRight();

            for (int k = 0; k <= 2; k++)
            {

                for (int i = 0; i <= 3; i++)
                {
                    MouseAction.SendMouseDragLeft(handler, window_Width, window_Height);
                    Thread.Sleep(delay);
                    if (Is_In_Turf() == false)
                    {
                        escPopUpAll_BruteForce();
                    }
                    if (Research_SearchForBuildingFound()) { return true; }
                }

                MouseAction.SendMouseDragUpSmall(handler, window_Width, window_Height, 50);
                Thread.Sleep(delay);
                if (Is_In_Turf() == false)
                {
                    escPopUpAll_BruteForce();
                }
                if (Research_SearchForBuildingFound()) { return true; }

                for (int i = 0; i <= 3; i++)
                {
                    MouseAction.SendMouseDragRight(handler, window_Width, window_Height);
                    Thread.Sleep(delay);
                    if (Is_In_Turf() == false)
                    {
                        escPopUpAll_BruteForce();
                    }
                    if (Research_SearchForBuildingFound()) { return true; }
                }
                if (k < 2)
                {
                    MouseAction.SendMouseDragUpSmall(handler, window_Width, window_Height, 50);
                    Thread.Sleep(delay);
                    if (Is_In_Turf() == false)
                    {
                        escPopUpAll_BruteForce();
                    }
                    if (Research_SearchForBuildingFound()) { return true; }
                }
            }

            return false;

   
        }
        private bool Research_SearchForBuilding()
        {
            worker.ReportProgress(ID, "Searching for research building.");
            hideUI();
            if (Research_SearchForBuildingFound()) { return true; }


            MouseAction.SendMouseDragUp(handler, window_Width, window_Height);
            Thread.Sleep(delay);
            if (Is_In_Turf() == false)
            {
                escPopUpAll_BruteForce();
            }
            for (int i = 0; i < 4; i++) {
                MouseAction.SendMouseDragLeft(handler, window_Width, window_Height);
                Thread.Sleep(delay/2);
                if (Is_In_Turf() == false)
                {
                    escPopUpAll_BruteForce();
                }
            }
            MouseAction.SendMouseDragRight(handler, window_Width, window_Height);
            Thread.Sleep(delay);
            if (Is_In_Turf() == false)
            {
                escPopUpAll_BruteForce();
            }
            MouseAction.SendMouseDragDownSmall(handler, window_Width, window_Height,140);
            Thread.Sleep(delay);
            if (Is_In_Turf() == false)
            {
                escPopUpAll_BruteForce();
            }

            if (Research_SearchForBuildingFound()) { return true; }

            return false;


        }
        private bool Research_SearchForBuildingFound()
        {

         
            double threshold_TEMP = 0.7;

            point = ImageProcessing.LocateImageSingle(handler, LordsBot.Properties.Resources.Research_Idle1, threshold_TEMP);
            if (point.X != 0 && point.Y != 0)
            {
                MouseAction.SendMouseClick(handler, point.X, point.Y);
                Thread.Sleep(delay);

                return true;
            }
            point = ImageProcessing.LocateImageSingle(handler, LordsBot.Properties.Resources.Research_Idle2, threshold_TEMP);
            if (point.X != 0 && point.Y != 0)
            {
                MouseAction.SendMouseClick(handler, point.X, point.Y);
                Thread.Sleep(delay);

                return true;
            }
            point = ImageProcessing.LocateImageSingle(handler, LordsBot.Properties.Resources.Research_Idle3, threshold_TEMP);
            if (point.X != 0 && point.Y != 0)
            {
                MouseAction.SendMouseClick(handler, point.X, point.Y);
                Thread.Sleep(delay);

                return true;
            }


            return false;

        }
        private bool Research_EconomyTree()
        {


            const double threshold_TEMP = 0.9;

            MouseAction.SendMouseDragUpSmall(handler, window_Width, window_Height, 100);
            Thread.Sleep(delay);

            //Econ Tab
            point = ImageProcessing.LocateImageSingle(handler, LordsBot.Properties.Resources.Research_EcoTree, threshold_TEMP);
            if (point.X != 0 && point.Y != 0)
            {
                MouseAction.SendMouseClick(handler, point.X, point.Y);
                Thread.Sleep(delay);
            }


            MouseAction.SendMouseDragUpSmall(handler, window_Width, window_Height, 100);
            Thread.Sleep(delay);

            //construction speed
            point = ImageProcessing.LocateImageSingle(handler, LordsBot.Properties.Resources.Research_EcoTree_Construction, threshold_TEMP);
            if (point.X != 0 && point.Y != 0)
            {
                MouseAction.SendMouseClick(handler, point.X, point.Y);
                Thread.Sleep(delay);

                Research_HitResearchButton1();

                    point = ImageProcessing.LocateImageSingle(handler, LordsBot.Properties.Resources.Research_Button2, threshold_TEMP);
                    if (point.X != 0 && point.Y != 0)
                    {
                        MouseAction.SendMouseClick(handler, point.X, point.Y);
                        Thread.Sleep(delay);

                        escPopUpAll_BruteForce();

                        return true;

                    }
                    else {
                        point = ImageProcessing.LocateImageSingle(handler, LordsBot.Properties.Resources.Research_Button3, threshold_TEMP);
                        if (point.X != 0 && point.Y != 0)
                        {

                            escPopUp();


                        }

                    }
                

            }

            //Food Harvesting
            point = ImageProcessing.LocateImageSingle(handler, LordsBot.Properties.Resources.Research_EcoTree_Food, threshold_TEMP);
            if (point.X != 0 && point.Y != 0)
            {
                MouseAction.SendMouseClick(handler, point.X, point.Y);
                Thread.Sleep(delay);

                Research_HitResearchButton1();

                    point = ImageProcessing.LocateImageSingle(handler, LordsBot.Properties.Resources.Research_Button2, threshold_TEMP);
                    if (point.X != 0 && point.Y != 0)
                    {
                        MouseAction.SendMouseClick(handler, point.X, point.Y);
                        Thread.Sleep(delay);

                        escPopUpAll_BruteForce();

                        return true;

                    }
                    else
                    {
                        point = ImageProcessing.LocateImageSingle(handler, LordsBot.Properties.Resources.Research_Button3, threshold_TEMP);
                        if (point.X != 0 && point.Y != 0)
                        {

                            escPopUp();


                        }

                    }
                

            }

            //Vault
            point = ImageProcessing.LocateImageSingle(handler, LordsBot.Properties.Resources.Research_EcoTree_Vault, threshold_TEMP);
            if (point.X != 0 && point.Y != 0)
            {
                MouseAction.SendMouseClick(handler, point.X, point.Y);
                Thread.Sleep(delay);

                Research_HitResearchButton1();

                    point = ImageProcessing.LocateImageSingle(handler, LordsBot.Properties.Resources.Research_Button2, threshold_TEMP);
                    if (point.X != 0 && point.Y != 0)
                    {
                        MouseAction.SendMouseClick(handler, point.X, point.Y);
                        Thread.Sleep(delay);

                        escPopUpAll_BruteForce();

                        return true;

                    }
                    else
                    {
                        point = ImageProcessing.LocateImageSingle(handler, LordsBot.Properties.Resources.Research_Button3, threshold_TEMP);
                        if (point.X != 0 && point.Y != 0)
                        {

                            escPopUp();


                        }

                    }
                

            }

            //Stone
            point = ImageProcessing.LocateImageSingle(handler, LordsBot.Properties.Resources.Research_EcoTree_Stone, threshold_TEMP);
            if (point.X != 0 && point.Y != 0)
            {
                MouseAction.SendMouseClick(handler, point.X, point.Y);
                Thread.Sleep(delay);

                Research_HitResearchButton1();

                    point = ImageProcessing.LocateImageSingle(handler, LordsBot.Properties.Resources.Research_Button2, threshold_TEMP);
                    if (point.X != 0 && point.Y != 0)
                    {
                        MouseAction.SendMouseClick(handler, point.X, point.Y);
                        Thread.Sleep(delay);

                        escPopUpAll_BruteForce();

                        return true;

                    }
                    else
                    {
                        point = ImageProcessing.LocateImageSingle(handler, LordsBot.Properties.Resources.Research_Button3, threshold_TEMP);
                        if (point.X != 0 && point.Y != 0)
                        {

                            escPopUp();


                        }

                    }
                

            }

            //Wood
            point = ImageProcessing.LocateImageSingle(handler, LordsBot.Properties.Resources.Research_EcoTree_Wood, threshold_TEMP);
            if (point.X != 0 && point.Y != 0)
            {
                MouseAction.SendMouseClick(handler, point.X, point.Y);
                Thread.Sleep(delay);

                Research_HitResearchButton1();

                    point = ImageProcessing.LocateImageSingle(handler, LordsBot.Properties.Resources.Research_Button2, threshold_TEMP);
                    if (point.X != 0 && point.Y != 0)
                    {
                        MouseAction.SendMouseClick(handler, point.X, point.Y);
                        Thread.Sleep(delay);

                        escPopUpAll_BruteForce();

                        return true;

                    }
                    else
                    {
                        point = ImageProcessing.LocateImageSingle(handler, LordsBot.Properties.Resources.Research_Button3, threshold_TEMP);
                        if (point.X != 0 && point.Y != 0)
                        {

                            escPopUp();


                        }

                    }
                

            }


            // Ore
            point = ImageProcessing.LocateImageSingle(handler, LordsBot.Properties.Resources.Research_EcoTree_Ore, threshold_TEMP);
            if (point.X != 0 && point.Y != 0)
            {
                MouseAction.SendMouseClick(handler, point.X, point.Y);
                Thread.Sleep(delay);

                Research_HitResearchButton1();

                    point = ImageProcessing.LocateImageSingle(handler, LordsBot.Properties.Resources.Research_Button2, threshold_TEMP);
                    if (point.X != 0 && point.Y != 0)
                    {
                        MouseAction.SendMouseClick(handler, point.X, point.Y);
                        Thread.Sleep(delay);

                        escPopUpAll_BruteForce();

                        return true;

                    }
                    else
                    {
                        point = ImageProcessing.LocateImageSingle(handler, LordsBot.Properties.Resources.Research_Button3, threshold_TEMP);
                        if (point.X != 0 && point.Y != 0)
                        {

                            escPopUp();


                        }

                    }
                

            }

            MouseAction.SendMouseDragDownSmall(handler, window_Width, window_Height, 250);
            Thread.Sleep(delay);


            // Weight Training
            point = ImageProcessing.LocateImageSingle(handler, LordsBot.Properties.Resources.Research_EcoTree_Weight, threshold_TEMP);
            if (point.X != 0 && point.Y != 0)
            {
                MouseAction.SendMouseClick(handler, point.X, point.Y);
                Thread.Sleep(delay);

                Research_HitResearchButton1();

                    point = ImageProcessing.LocateImageSingle(handler, LordsBot.Properties.Resources.Research_Button2, threshold_TEMP);
                    if (point.X != 0 && point.Y != 0)
                    {
                        MouseAction.SendMouseClick(handler, point.X, point.Y);
                        Thread.Sleep(delay);

                        escPopUpAll_BruteForce();

                        return true;

                    }
                    else
                    {
                        point = ImageProcessing.LocateImageSingle(handler, LordsBot.Properties.Resources.Research_Button3, threshold_TEMP);
                        if (point.X != 0 && point.Y != 0)
                        {

                            escPopUp();


                        }

                    }
                

            }

            // Gathering
            point = ImageProcessing.LocateImageSingle(handler, LordsBot.Properties.Resources.Research_EcoTree_Gathering, threshold_TEMP);
            if (point.X != 0 && point.Y != 0)
            {
                MouseAction.SendMouseClick(handler, point.X, point.Y);
                Thread.Sleep(delay);

                Research_HitResearchButton1();

                    point = ImageProcessing.LocateImageSingle(handler, LordsBot.Properties.Resources.Research_Button2, threshold_TEMP);
                    if (point.X != 0 && point.Y != 0)
                    {
                        MouseAction.SendMouseClick(handler, point.X, point.Y);
                        Thread.Sleep(delay);

                        escPopUpAll_BruteForce();

                        return true;

                    }
                    else
                    {
                        point = ImageProcessing.LocateImageSingle(handler, LordsBot.Properties.Resources.Research_Button3, threshold_TEMP);
                        if (point.X != 0 && point.Y != 0)
                        {

                            escPopUp();


                        }

                    }
                

            }

            escPopUp();

            return false;
        }
        private bool Research_MilitaryTree()
        {


            const double threshold_TEMP = 0.9;

            MouseAction.SendMouseDragUpSmall(handler, window_Width, window_Height, 100);
            Thread.Sleep(delay);

            //Military Tab
            point = ImageProcessing.LocateImageSingle(handler, LordsBot.Properties.Resources.Research_MilitaryTree, threshold_TEMP);
            if (point.X != 0 && point.Y != 0)
            {
                MouseAction.SendMouseClick(handler, point.X, point.Y);
                Thread.Sleep(delay);
            }


            MouseAction.SendMouseDragUpSmall(handler, window_Width, window_Height, 250);
            Thread.Sleep(delay);

            //Intel Report
            point = ImageProcessing.LocateImageSingle(handler, LordsBot.Properties.Resources.Research_MilitaryTree_Intel, threshold_TEMP);
            if (point.X != 0 && point.Y != 0)
            {
                MouseAction.SendMouseClick(handler, point.X, point.Y);
                Thread.Sleep(delay);

                Research_HitResearchButton1();

                point = ImageProcessing.LocateImageSingle(handler, LordsBot.Properties.Resources.Research_Button2, threshold_TEMP);
                if (point.X != 0 && point.Y != 0)
                {
                    MouseAction.SendMouseClick(handler, point.X, point.Y);
                    Thread.Sleep(delay);

                    escPopUpAll_BruteForce();

                    return true;

                }
                else
                {
                    point = ImageProcessing.LocateImageSingle(handler, LordsBot.Properties.Resources.Research_Button3, threshold_TEMP);
                    if (point.X != 0 && point.Y != 0)
                    {

                        escPopUp();


                    }

                }


            }

            //Quick Maneuvers
            point = ImageProcessing.LocateImageSingle(handler, LordsBot.Properties.Resources.Research_MilitaryTree_Quick, threshold_TEMP);
            if (point.X != 0 && point.Y != 0)
            {
                MouseAction.SendMouseClick(handler, point.X, point.Y);
                Thread.Sleep(delay);

                Research_HitResearchButton1();

                point = ImageProcessing.LocateImageSingle(handler, LordsBot.Properties.Resources.Research_Button2, threshold_TEMP);
                if (point.X != 0 && point.Y != 0)
                {
                    MouseAction.SendMouseClick(handler, point.X, point.Y);
                    Thread.Sleep(delay);

                    escPopUpAll_BruteForce();

                    return true;

                }
                else
                {
                    point = ImageProcessing.LocateImageSingle(handler, LordsBot.Properties.Resources.Research_Button3, threshold_TEMP);
                    if (point.X != 0 && point.Y != 0)
                    {

                        escPopUp();


                    }

                }


            }

            //move down
            MouseAction.SendMouseDragDownSmall_AccountSwitch(handler, window_Width, window_Height, 225);
            Thread.Sleep(delay + 1000);


            //Inf Offense
            point = ImageProcessing.LocateImageSingle(handler, LordsBot.Properties.Resources.Research_MilitaryTree_InfOff, threshold_TEMP);
            if (point.X != 0 && point.Y != 0)
            {
                MouseAction.SendMouseClick(handler, point.X, point.Y);
                Thread.Sleep(delay);

                Research_HitResearchButton1();

                point = ImageProcessing.LocateImageSingle(handler, LordsBot.Properties.Resources.Research_Button2, threshold_TEMP);
                if (point.X != 0 && point.Y != 0)
                {
                    MouseAction.SendMouseClick(handler, point.X, point.Y);
                    Thread.Sleep(delay);

                    escPopUpAll_BruteForce();

                    return true;

                }
                else
                {
                    point = ImageProcessing.LocateImageSingle(handler, LordsBot.Properties.Resources.Research_Button3, threshold_TEMP);
                    if (point.X != 0 && point.Y != 0)
                    {

                        escPopUp();


                    }

                }


            }

            //Siege Offense
            point = ImageProcessing.LocateImageSingle(handler, LordsBot.Properties.Resources.Research_MilitaryTree_SiegeOff, threshold_TEMP);
            if (point.X != 0 && point.Y != 0)
            {
                MouseAction.SendMouseClick(handler, point.X, point.Y);
                Thread.Sleep(delay);

                Research_HitResearchButton1();

                point = ImageProcessing.LocateImageSingle(handler, LordsBot.Properties.Resources.Research_Button2, threshold_TEMP);
                if (point.X != 0 && point.Y != 0)
                {
                    MouseAction.SendMouseClick(handler, point.X, point.Y);
                    Thread.Sleep(delay);

                    escPopUpAll_BruteForce();

                    return true;

                }
                else
                {
                    point = ImageProcessing.LocateImageSingle(handler, LordsBot.Properties.Resources.Research_Button3, threshold_TEMP);
                    if (point.X != 0 && point.Y != 0)
                    {

                        escPopUp();


                    }

                }


            }

            //Range Offense
            point = ImageProcessing.LocateImageSingle(handler, LordsBot.Properties.Resources.Research_MilitaryTree_RangeOff, threshold_TEMP);
            if (point.X != 0 && point.Y != 0)
            {
                MouseAction.SendMouseClick(handler, point.X, point.Y);
                Thread.Sleep(delay);

                Research_HitResearchButton1();

                point = ImageProcessing.LocateImageSingle(handler, LordsBot.Properties.Resources.Research_Button2, threshold_TEMP);
                if (point.X != 0 && point.Y != 0)
                {
                    MouseAction.SendMouseClick(handler, point.X, point.Y);
                    Thread.Sleep(delay);

                    escPopUpAll_BruteForce();

                    return true;

                }
                else
                {
                    point = ImageProcessing.LocateImageSingle(handler, LordsBot.Properties.Resources.Research_Button3, threshold_TEMP);
                    if (point.X != 0 && point.Y != 0)
                    {

                        escPopUp();


                    }

                }


            }


            //Cav Offense
            point = ImageProcessing.LocateImageSingle(handler, LordsBot.Properties.Resources.Research_MilitaryTree_CavOff, threshold_TEMP);
            if (point.X != 0 && point.Y != 0)
            {
                MouseAction.SendMouseClick(handler, point.X, point.Y);
                Thread.Sleep(delay);

                Research_HitResearchButton1();

                point = ImageProcessing.LocateImageSingle(handler, LordsBot.Properties.Resources.Research_Button2, threshold_TEMP);
                if (point.X != 0 && point.Y != 0)
                {
                    MouseAction.SendMouseClick(handler, point.X, point.Y);
                    Thread.Sleep(delay);

                    escPopUpAll_BruteForce();

                    return true;

                }
                else
                {
                    point = ImageProcessing.LocateImageSingle(handler, LordsBot.Properties.Resources.Research_Button3, threshold_TEMP);
                    if (point.X != 0 && point.Y != 0)
                    {

                        escPopUp();


                    }

                }


            }

            //Inf Defense
            point = ImageProcessing.LocateImageSingle(handler, LordsBot.Properties.Resources.Research_MilitaryTree_InfDef, threshold_TEMP);
            if (point.X != 0 && point.Y != 0)
            {
                MouseAction.SendMouseClick(handler, point.X, point.Y);
                Thread.Sleep(delay);

                Research_HitResearchButton1();

                point = ImageProcessing.LocateImageSingle(handler, LordsBot.Properties.Resources.Research_Button2, threshold_TEMP);
                if (point.X != 0 && point.Y != 0)
                {
                    MouseAction.SendMouseClick(handler, point.X, point.Y);
                    Thread.Sleep(delay);

                    escPopUpAll_BruteForce();

                    return true;

                }
                else
                {
                    point = ImageProcessing.LocateImageSingle(handler, LordsBot.Properties.Resources.Research_Button3, threshold_TEMP);
                    if (point.X != 0 && point.Y != 0)
                    {

                        escPopUp();


                    }

                }


            }

            //Siege Defense
            point = ImageProcessing.LocateImageSingle(handler, LordsBot.Properties.Resources.Research_MilitaryTree_SiegeDef, threshold_TEMP);
            if (point.X != 0 && point.Y != 0)
            {
                MouseAction.SendMouseClick(handler, point.X, point.Y);
                Thread.Sleep(delay);

                Research_HitResearchButton1();

                point = ImageProcessing.LocateImageSingle(handler, LordsBot.Properties.Resources.Research_Button2, threshold_TEMP);
                if (point.X != 0 && point.Y != 0)
                {
                    MouseAction.SendMouseClick(handler, point.X, point.Y);
                    Thread.Sleep(delay);

                    escPopUpAll_BruteForce();

                    return true;

                }
                else
                {
                    point = ImageProcessing.LocateImageSingle(handler, LordsBot.Properties.Resources.Research_Button3, threshold_TEMP);
                    if (point.X != 0 && point.Y != 0)
                    {

                        escPopUp();


                    }

                }


            }

            //Range Defense
            point = ImageProcessing.LocateImageSingle(handler, LordsBot.Properties.Resources.Research_MilitaryTree_RangeDef, threshold_TEMP);
            if (point.X != 0 && point.Y != 0)
            {
                MouseAction.SendMouseClick(handler, point.X, point.Y);
                Thread.Sleep(delay);

                Research_HitResearchButton1();

                point = ImageProcessing.LocateImageSingle(handler, LordsBot.Properties.Resources.Research_Button2, threshold_TEMP);
                if (point.X != 0 && point.Y != 0)
                {
                    MouseAction.SendMouseClick(handler, point.X, point.Y);
                    Thread.Sleep(delay);

                    escPopUpAll_BruteForce();

                    return true;

                }
                else
                {
                    point = ImageProcessing.LocateImageSingle(handler, LordsBot.Properties.Resources.Research_Button3, threshold_TEMP);
                    if (point.X != 0 && point.Y != 0)
                    {

                        escPopUp();


                    }

                }


            }


            //Cav Defense
            point = ImageProcessing.LocateImageSingle(handler, LordsBot.Properties.Resources.Research_MilitaryTree_CavDef, threshold_TEMP);
            if (point.X != 0 && point.Y != 0)
            {
                MouseAction.SendMouseClick(handler, point.X, point.Y);
                Thread.Sleep(delay);

                Research_HitResearchButton1();

                point = ImageProcessing.LocateImageSingle(handler, LordsBot.Properties.Resources.Research_Button2, threshold_TEMP);
                if (point.X != 0 && point.Y != 0)
                {
                    MouseAction.SendMouseClick(handler, point.X, point.Y);
                    Thread.Sleep(delay);

                    escPopUpAll_BruteForce();

                    return true;

                }
                else
                {
                    point = ImageProcessing.LocateImageSingle(handler, LordsBot.Properties.Resources.Research_Button3, threshold_TEMP);
                    if (point.X != 0 && point.Y != 0)
                    {

                        escPopUp();


                    }

                }


            }


            //move down
            MouseAction.SendMouseDragDownSmall_AccountSwitch(handler, window_Width, window_Height, 225);
            Thread.Sleep(delay + 1000);


            //Inf T2
            point = ImageProcessing.LocateImageSingle(handler, LordsBot.Properties.Resources.Research_MilitaryTree_T2Inf, threshold_TEMP);
            if (point.X != 0 && point.Y != 0)
            {
                MouseAction.SendMouseClick(handler, point.X, point.Y);
                Thread.Sleep(delay);

                Research_HitResearchButton1();

                point = ImageProcessing.LocateImageSingle(handler, LordsBot.Properties.Resources.Research_Button2, threshold_TEMP);
                if (point.X != 0 && point.Y != 0)
                {
                    MouseAction.SendMouseClick(handler, point.X, point.Y);
                    Thread.Sleep(delay);

                    escPopUpAll_BruteForce();

                    return true;

                }
                else
                {
                    point = ImageProcessing.LocateImageSingle(handler, LordsBot.Properties.Resources.Research_Button3, threshold_TEMP);
                    if (point.X != 0 && point.Y != 0)
                    {

                        escPopUp();


                    }

                }


            }

            //Siege T2
            point = ImageProcessing.LocateImageSingle(handler, LordsBot.Properties.Resources.Research_MilitaryTree_T2Siege, threshold_TEMP);
            if (point.X != 0 && point.Y != 0)
            {
                MouseAction.SendMouseClick(handler, point.X, point.Y);
                Thread.Sleep(delay);

                Research_HitResearchButton1();

                point = ImageProcessing.LocateImageSingle(handler, LordsBot.Properties.Resources.Research_Button2, threshold_TEMP);
                if (point.X != 0 && point.Y != 0)
                {
                    MouseAction.SendMouseClick(handler, point.X, point.Y);
                    Thread.Sleep(delay);

                    escPopUpAll_BruteForce();

                    return true;

                }
                else
                {
                    point = ImageProcessing.LocateImageSingle(handler, LordsBot.Properties.Resources.Research_Button3, threshold_TEMP);
                    if (point.X != 0 && point.Y != 0)
                    {

                        escPopUp();


                    }

                }


            }

            //Range T2
            point = ImageProcessing.LocateImageSingle(handler, LordsBot.Properties.Resources.Research_MilitaryTree_T2Range, threshold_TEMP);
            if (point.X != 0 && point.Y != 0)
            {
                MouseAction.SendMouseClick(handler, point.X, point.Y);
                Thread.Sleep(delay);

                Research_HitResearchButton1();

                point = ImageProcessing.LocateImageSingle(handler, LordsBot.Properties.Resources.Research_Button2, threshold_TEMP);
                if (point.X != 0 && point.Y != 0)
                {
                    MouseAction.SendMouseClick(handler, point.X, point.Y);
                    Thread.Sleep(delay);

                    escPopUpAll_BruteForce();

                    return true;

                }
                else
                {
                    point = ImageProcessing.LocateImageSingle(handler, LordsBot.Properties.Resources.Research_Button3, threshold_TEMP);
                    if (point.X != 0 && point.Y != 0)
                    {

                        escPopUp();


                    }

                }


            }


            //Cav T2
            point = ImageProcessing.LocateImageSingle(handler, LordsBot.Properties.Resources.Research_MilitaryTree_T2Cav, threshold_TEMP);
            if (point.X != 0 && point.Y != 0)
            {
                MouseAction.SendMouseClick(handler, point.X, point.Y);
                Thread.Sleep(delay);

                Research_HitResearchButton1();

                point = ImageProcessing.LocateImageSingle(handler, LordsBot.Properties.Resources.Research_Button2, threshold_TEMP);
                if (point.X != 0 && point.Y != 0)
                {
                    MouseAction.SendMouseClick(handler, point.X, point.Y);
                    Thread.Sleep(delay);

                    escPopUpAll_BruteForce();

                    return true;

                }
                else
                {
                    point = ImageProcessing.LocateImageSingle(handler, LordsBot.Properties.Resources.Research_Button3, threshold_TEMP);
                    if (point.X != 0 && point.Y != 0)
                    {

                        escPopUp();


                    }

                }


            }



            //Inf HP
            point = ImageProcessing.LocateImageSingle(handler, LordsBot.Properties.Resources.Research_MilitaryTree_InfHp, threshold_TEMP);
            if (point.X != 0 && point.Y != 0)
            {
                MouseAction.SendMouseClick(handler, point.X, point.Y);
                Thread.Sleep(delay);

                Research_HitResearchButton1();

                point = ImageProcessing.LocateImageSingle(handler, LordsBot.Properties.Resources.Research_Button2, threshold_TEMP);
                if (point.X != 0 && point.Y != 0)
                {
                    MouseAction.SendMouseClick(handler, point.X, point.Y);
                    Thread.Sleep(delay);

                    escPopUpAll_BruteForce();

                    return true;

                }
                else
                {
                    point = ImageProcessing.LocateImageSingle(handler, LordsBot.Properties.Resources.Research_Button3, threshold_TEMP);
                    if (point.X != 0 && point.Y != 0)
                    {

                        escPopUp();


                    }

                }


            }

            //Siege HP
            point = ImageProcessing.LocateImageSingle(handler, LordsBot.Properties.Resources.Research_MilitaryTree_SiegeHp, threshold_TEMP);
            if (point.X != 0 && point.Y != 0)
            {
                MouseAction.SendMouseClick(handler, point.X, point.Y);
                Thread.Sleep(delay);

                Research_HitResearchButton1();

                point = ImageProcessing.LocateImageSingle(handler, LordsBot.Properties.Resources.Research_Button2, threshold_TEMP);
                if (point.X != 0 && point.Y != 0)
                {
                    MouseAction.SendMouseClick(handler, point.X, point.Y);
                    Thread.Sleep(delay);

                    escPopUpAll_BruteForce();

                    return true;

                }
                else
                {
                    point = ImageProcessing.LocateImageSingle(handler, LordsBot.Properties.Resources.Research_Button3, threshold_TEMP);
                    if (point.X != 0 && point.Y != 0)
                    {

                        escPopUp();


                    }

                }


            }

            //Range HP
            point = ImageProcessing.LocateImageSingle(handler, LordsBot.Properties.Resources.Research_MilitaryTree_RangeHp, threshold_TEMP);
            if (point.X != 0 && point.Y != 0)
            {
                MouseAction.SendMouseClick(handler, point.X, point.Y);
                Thread.Sleep(delay);

                Research_HitResearchButton1();

                point = ImageProcessing.LocateImageSingle(handler, LordsBot.Properties.Resources.Research_Button2, threshold_TEMP);
                if (point.X != 0 && point.Y != 0)
                {
                    MouseAction.SendMouseClick(handler, point.X, point.Y);
                    Thread.Sleep(delay);

                    escPopUpAll_BruteForce();

                    return true;

                }
                else
                {
                    point = ImageProcessing.LocateImageSingle(handler, LordsBot.Properties.Resources.Research_Button3, threshold_TEMP);
                    if (point.X != 0 && point.Y != 0)
                    {

                        escPopUp();


                    }

                }


            }


            //Cav HP
            point = ImageProcessing.LocateImageSingle(handler, LordsBot.Properties.Resources.Research_MilitaryTree_CavHp, threshold_TEMP);
            if (point.X != 0 && point.Y != 0)
            {
                MouseAction.SendMouseClick(handler, point.X, point.Y);
                Thread.Sleep(delay);

                Research_HitResearchButton1();

                point = ImageProcessing.LocateImageSingle(handler, LordsBot.Properties.Resources.Research_Button2, threshold_TEMP);
                if (point.X != 0 && point.Y != 0)
                {
                    MouseAction.SendMouseClick(handler, point.X, point.Y);
                    Thread.Sleep(delay);

                    escPopUpAll_BruteForce();

                    return true;

                }
                else
                {
                    point = ImageProcessing.LocateImageSingle(handler, LordsBot.Properties.Resources.Research_Button3, threshold_TEMP);
                    if (point.X != 0 && point.Y != 0)
                    {

                        escPopUp();


                    }

                }


            }

            escPopUp();

            return false;
        }
        private void Research_HitResearchButton1() {
            MouseAction.SendMouseClick(handler, 451, 455);
            Thread.Sleep(delay);
        }




        private bool Gathering()
        {

            worker.ReportProgress(ID, "Starting Gathering.");

            initialGather = false;
            armyLimitReached = false;
            
            for (int i = 0; i < marchesGather && armyLimitReached == false && randomGather; i++)
            {
                HideUI_Map();
                Gathering_Search_Random2();



            }


            if (moveUpGather || moveDownGather || moveRightGather || moveLeftGather) {

                for (int i = 0; i < marchesGather && armyLimitReached == false ; i++)
                {

                    HideUI_Map();
                    Gathering_Search_Directional();


                }


            }




            escPopUpAll_BruteForce();
            UnHideUI_Map();

            return false;
        }

        /*private bool Gathering2() {

            
            Go_To_KingdomMap();

            HideUI_Map();
            armyLimitReached = false;
            for (int i = 0; i < 5 && armyLimitReached == false;i++) {
                randomNum = GetRandomNumber(1, 5);

                if (randomNum == 1) {

                    if (Gathering_Search1())
                    {

                        //Return_To_Castle_Location();
                       

                       
                    }
                }
                if (randomNum == 2)
                {

                    if (Gathering_Search2())
                    {

                        //Return_To_Castle_Location();
                       
                    }
                }
                if (randomNum == 3)
                {

                    if (Gathering_Search3())
                    {

                        //Return_To_Castle_Location();
                        
                    }
                }
                if (randomNum == 4)
                {

                    if (Gathering_Search4())
                    {

                        //Return_To_Castle_Location();
                        
                    }
                }

            }
            //Return_To_Castle_Location();
            escPopUpAll_BruteForce();
            UnHideUI_Map();

            return false;
        }*/
        /*private bool Gathering_Search1() {



            

            while (true) {

                for (int counter = 1; true; counter = counter + 2) {

                    for (int i = 0; i < counter; i++) {

                        MouseAction.SendMouseDragUp(handler, window_Width, window_Height);
                        Thread.Sleep(delay);
                        
                        if (Gathering_Gather()) {
                            return true;
                        }

                    }

                    for (int i = 0; i < counter; i++)
                    {
                        MouseAction.SendMouseDragLeft(handler, window_Width, window_Height);
                        Thread.Sleep(delay);
                        
                        if (Gathering_Gather())
                        {
                            return true;
                        }


                    }

                    for (int i = 0; i < counter + 1 ; i++)
                    {
                        MouseAction.SendMouseDragDown(handler, window_Width, window_Height);
                        Thread.Sleep(delay);
                        
                        if (Gathering_Gather())
                        {
                            return true;
                        }


                    }

                    for (int i = 0; i < counter + 1; i++)
                    {
                        MouseAction.SendMouseDragRight(handler, window_Width, window_Height);
                        Thread.Sleep(delay);

                       
                        if (Gathering_Gather())
                        {
                            return true;
                        }


                    }

                    if (counter > 5) {
                        return false;
                    }

                }

            }
        }*/
        /*private bool Gathering_Search2()
        {



            

            while (true)
            {

                for (int counter = 1; true; counter = counter + 2)
                {

                    for (int i = 0; i < counter; i++)
                    {

                        MouseAction.SendMouseDragDown(handler, window_Width, window_Height);
                        Thread.Sleep(delay);
                        
                        if (Gathering_Gather())
                        {
                            return true;
                        }

                    }

                    for (int i = 0; i < counter; i++)
                    {
                        MouseAction.SendMouseDragLeft(handler, window_Width, window_Height);
                        Thread.Sleep(delay);
                        
                        if (Gathering_Gather())
                        {
                            return true;
                        }


                    }

                    for (int i = 0; i < counter + 1; i++)
                    {
                        MouseAction.SendMouseDragUp(handler, window_Width, window_Height);
                        Thread.Sleep(delay);
                        
                        if (Gathering_Gather())
                        {
                            return true;
                        }



                    }

                    for (int i = 0; i < counter + 1; i++)
                    {
                        MouseAction.SendMouseDragRight(handler, window_Width, window_Height);
                        Thread.Sleep(delay);

                        
                        if (Gathering_Gather())
                        {
                            return true;
                        }


                    }

                    if (counter > 5)
                    {
                        return false;
                    }

                }

            }
        }*/
        /*private bool Gathering_Search3()
        {



            

            while (true)
            {

                for (int counter = 1; true; counter = counter + 2)
                {

                    for (int i = 0; i < counter; i++)
                    {

                     
                        MouseAction.SendMouseDragRight(handler, window_Width, window_Height);
                        Thread.Sleep(delay);

                        
                        if (Gathering_Gather())
                        {
                            return true;
                        }

                    }

                    for (int i = 0; i < counter; i++) { 
                    
                        MouseAction.SendMouseDragDown(handler, window_Width, window_Height);
                        Thread.Sleep(delay);
                        
                        if (Gathering_Gather())
                        {
                            return true;
                        }


                    }

                    for (int i = 0; i < counter + 1; i++)
                    {
                      

                        MouseAction.SendMouseDragLeft(handler, window_Width, window_Height);
                        Thread.Sleep(delay);
                        
                        if (Gathering_Gather())
                        {
                            return true;
                        }

                    }

                    for (int i = 0; i < counter + 1; i++)
                    {

                        MouseAction.SendMouseDragUp(handler, window_Width, window_Height);
                        Thread.Sleep(delay);
                        
                        if (Gathering_Gather())
                        {
                            return true;
                        }


                    }

                    if (counter > 5)
                    {
                        return false;
                    }

                }

            }
        }*/
        /*private bool Gathering_Search4()
        {




            while (true)
            {

                for (int counter = 1; true; counter = counter + 2)
                {

                    for (int i = 0; i < counter; i++)
                    {


                      


                        MouseAction.SendMouseDragLeft(handler, window_Width, window_Height);
                        Thread.Sleep(delay);
                        
                        if (Gathering_Gather())
                        {
                            return true;
                        }
                    }

                    for (int i = 0; i < counter; i++)
                    {

                        MouseAction.SendMouseDragDown(handler, window_Width, window_Height);
                        Thread.Sleep(delay);
                        
                        if (Gathering_Gather())
                        {
                            return true;
                        }


                    }

                    for (int i = 0; i < counter + 1; i++)
                    {


                        MouseAction.SendMouseDragRight(handler, window_Width, window_Height);
                        Thread.Sleep(delay);

                        
                        if (Gathering_Gather())
                        {
                            return true;
                        }

                    }

                    for (int i = 0; i < counter + 1; i++)
                    {

                        MouseAction.SendMouseDragUp(handler, window_Width, window_Height);
                        Thread.Sleep(delay);
                        
                        if (Gathering_Gather())
                        {
                            return true;
                        }


                    }

                    if (counter > 5)
                    {
                        return false;
                    }

                }

            }
        }*/
        /*private bool Gathering_Search_Random()
        {

            for (int i = 0; i < 10;i++) {
                randomNum = GetRandomNumber(1, 5);

                if (randomNum == 1) {
                    MouseAction.SendMouseDragUp(handler, window_Width, window_Height);
                    Thread.Sleep(delay);

                    if (Gathering_Gather())
                    {
                        return true;
                    }
                }
                if (randomNum == 2) {
                    MouseAction.SendMouseDragLeft(handler, window_Width, window_Height);
                    Thread.Sleep(delay);

                    if (Gathering_Gather())
                    {
                        return true;
                    }
                }
                if (randomNum == 3) {
                    MouseAction.SendMouseDragDown(handler, window_Width, window_Height);
                    Thread.Sleep(delay);

                    if (Gathering_Gather())
                    {
                        return true;
                    }
                }
                if (randomNum == 4) {
                    MouseAction.SendMouseDragRight(handler, window_Width, window_Height);
                    Thread.Sleep(delay);


                    if (Gathering_Gather())
                    {
                        return true;
                    }
                }

            }

            return false;
        }*/

        private bool Gathering_Search_Random2()
        {

            if (initialGather == false) {
                initialGather = true;
                if (Gathering_Gather())
                {
                    return true;
                }
            }

            for (int i = 0; i < searchDistance; i++)
            {
                randomNum = GetRandomNumber(1, 5);

                if (randomNum == 1)
                {
                    randomNum = GetRandomNumber(1, 3);
                    for (int k = 0;k < randomNum;k++) {
                        MouseAction.SendMouseDragUp(handler, window_Width, window_Height);
                        Thread.Sleep(delay);
                    }
                    if (Gathering_Gather())
                    {
                        return true;
                    }
                }
                if (randomNum == 2)
                {
                    randomNum = GetRandomNumber(1, 3);
                    for (int k = 0; k < randomNum; k++)
                    {
                        MouseAction.SendMouseDragLeft(handler, window_Width, window_Height);
                        Thread.Sleep(delay);
                    }
                    if (Gathering_Gather())
                    {
                        return true;
                    }
                }
                if (randomNum == 3)
                {
                    randomNum = GetRandomNumber(1, 3);
                    for (int k = 0; k < randomNum; k++)
                    {
                        MouseAction.SendMouseDragDown(handler, window_Width, window_Height);
                        Thread.Sleep(delay);
                    }
                    if (Gathering_Gather())
                    {
                        return true;
                    }
                }
                if (randomNum == 4)
                {
                    randomNum = GetRandomNumber(1, 3);
                    for (int k = 0; k < randomNum; k++)
                    {
                        MouseAction.SendMouseDragRight(handler, window_Width, window_Height);
                        Thread.Sleep(delay);
                    }

                    if (Gathering_Gather())
                    {
                        return true;
                    }
                }

            }
            escPopUpAll_BruteForce();
            return false;
        }

        private bool Gathering_Search_Directional()
        {

            if (initialGather == false)
            {
                initialGather = true;
                if (Gathering_Gather())
                {
                    return true;
                }
            }

            for (int i = 0; i < searchDistance; i++)
            {
                randomNum = GetRandomNumber(1, 5);

                if (randomNum == 1 && moveUpGather)
                {

                    Gathering_Search_Up();
                }
                if (randomNum == 2 && moveLeftGather)
                {

                    Gathering_Search_Left();
                }
                if (randomNum == 3 && moveDownGather)
                {

                    Gathering_Search_Down();
                }
                if (randomNum == 4 && moveRightGather)
                {

                    Gathering_Search_Right();
                }

            }

            return false;
        }
        private bool Gathering_Search_Up() {

 
                MouseAction.SendMouseDragUp(handler, window_Width, window_Height);
                Thread.Sleep(delay);
            
            if (Gathering_Gather())
            {
                return true;
            }


            return false;
        }
        private bool Gathering_Search_Down()
        {


            MouseAction.SendMouseDragDown(handler, window_Width, window_Height);
            Thread.Sleep(delay);

            if (Gathering_Gather())
            {
                return true;
            }


            return false;
        }
        private bool Gathering_Search_Right()
        {


            MouseAction.SendMouseDragRight(handler, window_Width, window_Height);
            Thread.Sleep(delay);

            if (Gathering_Gather())
            {
                return true;
            }


            return false;
        }
        private bool Gathering_Search_Left()
        {


            MouseAction.SendMouseDragLeft(handler, window_Width, window_Height);
            Thread.Sleep(delay);

            if (Gathering_Gather())
            {
                return true;
            }


            return false;
        }

        private bool Gathering_Gather()
        {


            bool goldChecked = !goldGather;
            bool woodChecked = !woodGather;
            bool stoneChecked = !stoneGather;
            bool oreChecked = !oreGather;
            bool foodChecked = !foodGather;

            int errorCounter = 0;

            do
            {
                randomNum = GetRandomNumber(1, 6);
                if (randomNum == 1 && goldChecked == false)
                {
                    if (Gathering_Search_Gold()) { return true; }
                    goldChecked = true;
                }

                if (randomNum == 2 && woodChecked == false)
                {
                    if (Gathering_Search_Wood()) { return true; }
                    woodChecked = true;
                }

                if (randomNum == 3 && stoneChecked == false)
                {
                    if (Gathering_Search_Stone()) { return true; }
                    stoneChecked = true;
                }

                if (randomNum == 4 && oreChecked == false)
                {
                    if (Gathering_Search_Ore()) { return true; }
                    oreChecked = true;
                }

                if (randomNum == 5 && foodChecked == false)
                {
                    if (Gathering_Search_Food()) { return true; }
                    foodChecked = true;

                }
                errorCounter++;
                if (goldChecked && woodChecked  && stoneChecked  && oreChecked  && foodChecked ) {

                    break;
                }               
                if (errorCounter > 50) { break; }
            } while (true);


            return false;
        }        
        private bool Gathering_Search_Gold() {
            if (Gathering_Search_Tile_Gold())
            {

                if (Gathering_Launch_Gathers())
                {
                    return true;
                }

            }
            return false;
        }
        private bool Gathering_Search_Tile_Gold() {


            double threshold_TEMP = 0.9;

            

            for (int i = 0; i < 3; i++)
            {

                point = ImageProcessing.LocateImageSingle(handler, LordsBot.Properties.Resources.Gold_Forest, threshold_TEMP);
                if (point.X != 0 && point.Y != 0)
                {
                    MouseAction.SendMouseClick(handler, point.X, point.Y);
                    Thread.Sleep(delay);
                    return true;
                }

                point = ImageProcessing.LocateImageSingle(handler, LordsBot.Properties.Resources.Gold_MagmaPath, threshold_TEMP);
                if (point.X != 0 && point.Y != 0)
                {
                    MouseAction.SendMouseClick(handler, point.X, point.Y);
                    Thread.Sleep(delay);
                    return true;
                }

                point = ImageProcessing.LocateImageSingle(handler, LordsBot.Properties.Resources.Gold_Snow, threshold_TEMP);
                if (point.X != 0 && point.Y != 0)
                {
                    MouseAction.SendMouseClick(handler, point.X, point.Y);
                    Thread.Sleep(delay);
                    return true;
                }

            }

            return false;
        }
        private bool Gathering_Search_Wood() {
            if (Gathering_Search_Tile_Wood())
            {

                if (Gathering_Launch_Gathers())
                {
                    return true;
                }

            }
            return false;
        }
        private bool Gathering_Search_Tile_Wood() {

            double threshold_TEMP = 0.9;

            

            for (int i = 0; i < 3; i++) {

                point = ImageProcessing.LocateImageSingle(handler, LordsBot.Properties.Resources.Wood_Forest, threshold_TEMP);
                if (point.X != 0 && point.Y != 0)
                {
                    MouseAction.SendMouseClick(handler, point.X, point.Y);
                    Thread.Sleep(delay);
                    return true;
                }

                point = ImageProcessing.LocateImageSingle(handler, LordsBot.Properties.Resources.Wood_MagmaPath, threshold_TEMP);
                if (point.X != 0 && point.Y != 0)
                {
                    MouseAction.SendMouseClick(handler, point.X, point.Y);
                    Thread.Sleep(delay);
                    return true;
                }

                point = ImageProcessing.LocateImageSingle(handler, LordsBot.Properties.Resources.Wood_Snow, threshold_TEMP);
                if (point.X != 0 && point.Y != 0)
                {
                    MouseAction.SendMouseClick(handler, point.X, point.Y);
                    Thread.Sleep(delay);
                    return true;
                }

            }

            return false;

        }
        private bool Gathering_Search_Stone() {
            if (Gathering_Search_Tile_Stone())
            {

                if (Gathering_Launch_Gathers())
                {
                    return true;
                }

            }
            return false;
        }
        private bool Gathering_Search_Tile_Stone() {


            double threshold_TEMP = 0.9;

           

            for (int i = 0; i < 3; i++)
            {

                point = ImageProcessing.LocateImageSingle(handler, LordsBot.Properties.Resources.Stone_Forest, threshold_TEMP);
                if (point.X != 0 && point.Y != 0)
                {
                    MouseAction.SendMouseClick(handler, point.X, point.Y);
                    Thread.Sleep(delay);
                    return true;
                }

                point = ImageProcessing.LocateImageSingle(handler, LordsBot.Properties.Resources.Stone_MagmaPath, threshold_TEMP);
                if (point.X != 0 && point.Y != 0)
                {
                    MouseAction.SendMouseClick(handler, point.X, point.Y);
                    Thread.Sleep(delay);
                    return true;
                }

                point = ImageProcessing.LocateImageSingle(handler, LordsBot.Properties.Resources.Stone_Snow, threshold_TEMP);
                if (point.X != 0 && point.Y != 0)
                {
                    MouseAction.SendMouseClick(handler, point.X, point.Y);
                    Thread.Sleep(delay);
                    return true;
                }

            }

            return false;


        }
        private bool Gathering_Search_Ore() {
            if (Gathering_Search_Tile_Ore())
            {

                if (Gathering_Launch_Gathers())
                {
                    return true;
                }

            }
            return false;
        }
        private bool Gathering_Search_Tile_Ore() {


            double threshold_TEMP = 0.9;

           

            for (int i = 0; i < 3; i++)
            {

                point = ImageProcessing.LocateImageSingle(handler, LordsBot.Properties.Resources.Ore_Forest, threshold_TEMP);
                if (point.X != 0 && point.Y != 0)
                {
                    MouseAction.SendMouseClick(handler, point.X, point.Y);
                    Thread.Sleep(delay);
                    return true;
                }

                point = ImageProcessing.LocateImageSingle(handler, LordsBot.Properties.Resources.Ore_MagmaPath, threshold_TEMP);
                if (point.X != 0 && point.Y != 0)
                {
                    MouseAction.SendMouseClick(handler, point.X, point.Y);
                    Thread.Sleep(delay);
                    return true;
                }

                point = ImageProcessing.LocateImageSingle(handler, LordsBot.Properties.Resources.Ore_Snow, threshold_TEMP);
                if (point.X != 0 && point.Y != 0)
                {
                    MouseAction.SendMouseClick(handler, point.X, point.Y);
                    Thread.Sleep(delay);
                    return true;
                }

            }

            return false;

        }
        private bool Gathering_Search_Food() {

            if (Gathering_Search_Tile_Food())
            {

                if (Gathering_Launch_Gathers())
                {
                    return true;
                }

            }
            return false;
        }
        private bool Gathering_Search_Tile_Food()
        {


            double threshold_TEMP = 0.9;

           

            for (int i = 0; i < 3; i++)
            {

                point = ImageProcessing.LocateImageSingle(handler, LordsBot.Properties.Resources.Food, threshold_TEMP);
                if (point.X != 0 && point.Y != 0)
                {
                    MouseAction.SendMouseClick(handler, point.X, point.Y);
                    Thread.Sleep(delay);
                    return true;
                }
            

            }

            return false;

        }
        private bool Gathering_Launch_Gathers() {

         
            if (Gathering_Launch_HitGathersButton())
            {
                

                if (Army_Limit_Reached()) {
                    armyLimitReached = true;
                    return true;
                }


                if (Gathering_Launch_Gathers_AutoAssemble()) {
                    return true;
                }

                if (Gathering_Launch_Gathers_LowestTier())
                {

                    return true;
                }

                if (Gathering_Launch_Gathers_Select_LowestTier())
                {
                    return true;
                }



            }


            return false;
        }
        private bool Gathering_Launch_HitGathersButton() {
            const double threshold_TEMP = 0.7;

            point = ImageProcessing.LocateImageSingle(handler, LordsBot.Properties.Resources.Gather_Button, threshold_TEMP);
            if (point.X != 0 && point.Y != 0)
            {
                MouseAction.SendMouseClick(handler, point.X, point.Y);
                Thread.Sleep(delay);
                return true;
            }
            point = ImageProcessing.LocateImageSingle(handler, LordsBot.Properties.Resources.Gather_Button2, threshold_TEMP);
            if (point.X != 0 && point.Y != 0)
            {
                MouseAction.SendMouseClick(handler, point.X, point.Y);
                Thread.Sleep(delay);
                return true;
            }


            return false;
        }
        private bool Gathering_Launch_Gathers_AutoAssemble() {

            double threshold_temp = 0.75;

            point = ImageProcessing.LocateImageSingle(handler, LordsBot.Properties.Resources.Auto_Assemble, threshold_temp);
            if (point.X != 0 && point.Y != 0)
            {
                MouseAction.SendMouseClick(handler, point.X, point.Y);
                Thread.Sleep(delay);

                if (Gathering_Launch_Gathers_Button())
                {
                    return true;
                }


            }

            return false;
        }
        private bool Gathering_Launch_Gathers_LowestTier() {

           

            point = ImageProcessing.LocateImageSingle(handler, LordsBot.Properties.Resources.Gather_LowestTier, threshold);
            if (point.X != 0 && point.Y != 0)
            {
                MouseAction.SendMouseClick(handler, point.X, point.Y);
                Thread.Sleep(delay);

                if (Gathering_Launch_Gathers_Button()) {
                    return true;
                }


            }

            return false;


        }
        private bool Gathering_Launch_Gathers_Select_LowestTier() {

            

            point = ImageProcessing.LocateImageSingle(handler, LordsBot.Properties.Resources.Gather_OpenOptionTab, threshold);
            if (point.X != 0 && point.Y != 0)
            {
                MouseAction.SendMouseClick(handler, point.X, point.Y);
                Thread.Sleep(delay);


                point = ImageProcessing.LocateImageSingle(handler, LordsBot.Properties.Resources.Gather_LowestTierFirst, threshold);
                if (point.X != 0 && point.Y != 0)
                {
                    MouseAction.SendMouseClick(handler, point.X, point.Y);
                    Thread.Sleep(delay);

                    if (Gathering_Launch_Gathers_Button()) {
                        return true;
                    }

                }

            }
            return false;

        }
        private bool Gathering_Launch_Gathers_Button() {
           


            point = ImageProcessing.LocateImageSingle(handler, LordsBot.Properties.Resources.LaunchArmy, threshold);
            if (point.X != 0 && point.Y != 0)
            {
                MouseAction.SendMouseClick(handler, point.X, point.Y);
                Thread.Sleep(delay*2);

                //check if still in launch army screen, if true it means not enough troops to send out.
                point = ImageProcessing.LocateImageSingle(handler, LordsBot.Properties.Resources.LaunchArmy, threshold);
                if (point.X != 0 && point.Y != 0)
                {
                    armyLimitReached = true;
                }
                    escPopUp();
                return true;
            }
            return false;
        }


        private bool Transfer_Resource() {


            if (account_Count == 1) {

                if (transfer_FreqCount == 1)
                {
                    transfer_FreqCheck = true;
                }
                else {
                    transfer_FreqCheck = false;
                }


                transfer_FreqCount = transfer_FreqCount + 1;
                if (transfer_FreqCount > transferFrequency) {
                    transfer_FreqCount = 1;
                }
            }

            account_Count = account_Count + 1;
            if (account_Count > AccountSwitch_Count) {
                account_Count = 1;
            }


            if (transfer_FreqCheck == false) {
                return true;
            }
        


            worker.ReportProgress(ID, "Starting Resource Transfer.");

            if (foodTransfer == true) {
                for (int i = 0; i < marchesTransferFood;i++) {
                    if (Transfer_Resource_Manager(1) == false) {
                        break;
                    }
                }
            }
            if (stoneTransfer == true)
            {
                for (int i = 0; i < marchesTransferStone; i++)
                {
                    if(Transfer_Resource_Manager(2) == false)
                    {
                        return false;
                    }
                }
            }
            if (woodTransfer == true)
            {
                for (int i = 0; i < marchesTransferWood; i++)
                {
                    if (Transfer_Resource_Manager(3) == false)
                    {
                        return false;
                    }
            }
            }
            if (oreTransfer == true)
            {
                for (int i = 0; i < marchesTransferOre; i++)
                {
                    if(Transfer_Resource_Manager(4) == false)
                    {
                        return false;
                    }
                }
            }
            if (goldTransfer == true)
            {
                for (int i = 0; i < marchesTransferGold; i++)
                {
                    if(Transfer_Resource_Manager(5) == false)
                    {
                        return false;
                    }
                }
            }

            return false;
        }

        private bool Transfer_Resource_Manager(int resouce) {

            for (int i = 0; i < 10; i++)
            {
                if (Transfer_Resource_Open_Bookmarks())
                {

                    if (Transfer_Resource_Open_Bookmark_NumOne())
                    {

                        Transfer_Resource_Click_Castle();

                        if (Transfer_Resource_Click_Send_Resource())
                        {

                            if (Transfer_Resource_Supply_Resource(resouce))
                            {

                                if (Transfer_Resource_Click_Supply())
                                {
                                    if (Transfer_Resource_Click_Confirm())
                                    {
                                        return true;
                                    }
                                }
                            }
                            else
                            {
                                //wait 1 minute before retry
                                Thread.Sleep(60000);
                            }

                        }
                        else if (Transfer_Resource_CheckIfOwnCastle())
                        {
                            escPopUpAll_BruteForce();
                            return false;
                        }

                    }
                }
                escPopUpAll_BruteForce();
            }
            return false;
        }
        private bool Transfer_Resource_Open_Bookmarks() {

            const double threshold_TEMP = 0.8;

            point = ImageProcessing.LocateImageSingle(handler, LordsBot.Properties.Resources.Supply_Bookmark, threshold_TEMP);
            if (point.X != 0 && point.Y != 0)
            {
                MouseAction.SendMouseClick(handler, point.X, point.Y);
                Thread.Sleep(delay);
                return true;
            }

            return false;
        }
        private bool Transfer_Resource_Open_Bookmark_NumOne()
        {
            //opens Guild book marks
            if (useGuildBookmarks) {
                MouseAction.SendMouseClick(handler, 755, 262);
                Thread.Sleep(delay);
            }

            //click on bookmark number one
            const double threshold_TEMP = 0.7;

            point = ImageProcessing.LocateImageSingle(handler, LordsBot.Properties.Resources.Supply_Bookmark_View, threshold_TEMP);
            if (point.X != 0 && point.Y != 0)
            {
                MouseAction.SendMouseClick(handler, 635, 177);
                Thread.Sleep(delay);
                return true;
            }

            return false;
        }
        private void Transfer_Resource_Click_Castle()
        {

           //the castle should be at center of screen
           
                MouseAction.SendMouseClick(handler, 454, 275);
                Thread.Sleep(delay);
              
        }
        private bool Transfer_Resource_CheckIfOwnCastle()
        {

            const double threshold_TEMP = 0.75;

            point = ImageProcessing.LocateImageSingle(handler, LordsBot.Properties.Resources.CheckIfOwnCastle, threshold_TEMP);
            if (point.X != 0 && point.Y != 0)
            {                
                return true;
            }

            return false;
        }
        private bool Transfer_Resource_Click_Send_Resource()
        {

            
            const double threshold_TEMP = 0.7;

            point = ImageProcessing.LocateImageSingle(handler, LordsBot.Properties.Resources.Supply_Send_Resources, threshold_TEMP);
            if (point.X != 0 && point.Y != 0)
            {
                MouseAction.SendMouseClick(handler, point.X, point.Y);
                Thread.Sleep(delay);
                return true;
            }

            return false;
        }
        private bool Transfer_Resource_Click_Supply()
        {

            const double threshold_TEMP = 0.8;

            point = ImageProcessing.LocateImageSingle(handler, LordsBot.Properties.Resources.Supply_Supply, threshold_TEMP);
            if (point.X != 0 && point.Y != 0)
            {
                MouseAction.SendMouseClick(handler, point.X, point.Y);
                Thread.Sleep(delay);
                return true;
            }
            //return false if can't supply
            point = ImageProcessing.LocateImageSingle(handler, LordsBot.Properties.Resources.Supply_Supply_Red, threshold_TEMP);
            if (point.X != 0 && point.Y != 0)
            {
                MouseAction.SendMouseClick(handler, point.X, point.Y);
                Thread.Sleep(delay);
                return false;
            }


            return false;
        }
        private bool Transfer_Resource_Click_Confirm()
        {

            const double threshold_TEMP = 0.7;

            point = ImageProcessing.LocateImageSingle(handler, LordsBot.Properties.Resources.Supply_Confirm, threshold_TEMP);
            if (point.X != 0 && point.Y != 0)
            {
                MouseAction.SendMouseClick(handler, point.X, point.Y);
                Thread.Sleep(delay);
                return true;
            }


            return false;
        }
        private bool Transfer_Resource_Supply_Resource(int resource) {
        
                if (resource == 1) {
                    if (Transfer_Resource_Supply_Food()) {
                        return true;
                    }
                }
                if (resource == 2)
                {
                    if (Transfer_Resource_Supply_Stone())
                    {
                        return true;
                    }
                }
                if (resource == 3)
                {
                    if (Transfer_Resource_Supply_Wood())
                    {
                        return true;
                    }
                }
                if (resource == 4)
                {
                    if (Transfer_Resource_Supply_Ore())
                    {
                        return true;
                    }
                }
                if (resource == 5)
                {
                    if (Transfer_Resource_Supply_Gold())
                    {
                        return true;
                    }
                }


            

            return false;
        }
        private bool Transfer_Resource_Supply_Food()
        {

            const double threshold_TEMP = 0.8;

            point = ImageProcessing.LocateImageSingle(handler, LordsBot.Properties.Resources.Supply_Food, threshold_TEMP);
            if (point.X != 0 && point.Y != 0)
            {
                MouseAction.SendMouseClick(handler, point.X, point.Y);
                Thread.Sleep(delay);
                return true;
            }


            return false;
        }
        private bool Transfer_Resource_Supply_Stone()
        {
            const double threshold_TEMP = 0.8;

            point = ImageProcessing.LocateImageSingle(handler, LordsBot.Properties.Resources.Supply_Stone, threshold_TEMP);
            if (point.X != 0 && point.Y != 0)
            {
                MouseAction.SendMouseClick(handler, point.X, point.Y);
                Thread.Sleep(delay);
                return true;
            }
            return false;
        }
        private bool Transfer_Resource_Supply_Wood()
        {

            const double threshold_TEMP = 0.8;

            point = ImageProcessing.LocateImageSingle(handler, LordsBot.Properties.Resources.Supply_Wood, threshold_TEMP);
            if (point.X != 0 && point.Y != 0)
            {
                MouseAction.SendMouseClick(handler, point.X, point.Y);
                Thread.Sleep(delay);
                return true;
            }

            return false;
        }
        private bool Transfer_Resource_Supply_Ore()
        {
            const double threshold_TEMP = 0.8;

            point = ImageProcessing.LocateImageSingle(handler, LordsBot.Properties.Resources.Supply_Ore, threshold_TEMP);
            if (point.X != 0 && point.Y != 0)
            {
                MouseAction.SendMouseClick(handler, point.X, point.Y);
                Thread.Sleep(delay);
                return true;
            }

            return false;
        }
        private bool Transfer_Resource_Supply_Gold()
        {
            const double threshold_TEMP = 0.8;

            point = ImageProcessing.LocateImageSingle(handler, LordsBot.Properties.Resources.Supply_Gold, threshold_TEMP);
            if (point.X != 0 && point.Y != 0)
            {
                MouseAction.SendMouseClick(handler, point.X, point.Y);
                Thread.Sleep(delay);
                return true;
            }

            return false;
        }


        private bool Army_Limit_Reached()
        {
            const double threshold_TEMP = 0.7;
         
            point = ImageProcessing.LocateImageSingle(handler, LordsBot.Properties.Resources.Army_Limit, threshold_TEMP);
            if (point.X != 0 && point.Y != 0)
            {
                worker.ReportProgress(ID, "Army Limit Reached.");
                escPopUpAll_BruteForce();
                return true;
            }
            

            return false;
        }
        private bool isInGame()
        {
            
            point = ImageProcessing.LocateImageSingle(handler, LordsBot.Properties.Resources.isInGame, threshold);
            if (point.X != 0 && point.Y != 0)
            {
                return true;
            }

            return false;
        }
        private bool RequestHelp()
        {

            const double threshold_TEMP = 0.7;

           

            point = ImageProcessing.LocateImageSingle(handler, LordsBot.Properties.Resources.requestHelp, threshold_TEMP);
            if (point.X != 0 && point.Y != 0)
            {
                MouseAction.SendMouseClick(handler, point.X, point.Y);
                worker.ReportProgress(ID, "Requesting helps.");
                Thread.Sleep(delay);
                return true;
            }

            return false;
        }
        private bool PressFreeButton()
        {
            const double threshold_TEMP = 0.7;

            point = ImageProcessing.LocateImageSingle(handler, LordsBot.Properties.Resources.FreeButton, threshold_TEMP);
            if (point.X != 0 && point.Y != 0)
            {
                MouseAction.SendMouseClick(handler, point.X, point.Y);
                worker.ReportProgress(ID, "Clicked \"Free\" button");
                Thread.Sleep(delay);
                return true;
            }

            return false;
        }
        private bool CheckLevelUp()
        {
            

            point = ImageProcessing.LocateImageSingle(handler, LordsBot.Properties.Resources.LevelUp1, 0.8);
            if (point.X != 0 && point.Y != 0)
            {

                

                        MouseAction.SendMouseClick(handler, 614, 62); //hit exit button
                        Thread.Sleep(delay);

                        worker.ReportProgress(ID, "Leveled up!");


                        
                return true;
            }
            
             point = ImageProcessing.LocateImageSingle(handler, LordsBot.Properties.Resources.LevelUp2, 0.8);
                if (point.X != 0 && point.Y != 0)
                {



                         MouseAction.SendMouseClick(handler, 614, 62); //hit exit button
                         Thread.Sleep(delay);

                            worker.ReportProgress(ID, "Leveled up!");

                            

                          return true;
                        

                    
                }
            point = ImageProcessing.LocateImageSingle(handler, LordsBot.Properties.Resources.LevelUp3, 0.8);
            if (point.X != 0 && point.Y != 0)
            {



                MouseAction.SendMouseClick(handler, 614, 62); //hit exit button
                Thread.Sleep(delay);

                worker.ReportProgress(ID, "Leveled up!");



                return true;



            }
            return false;
        }
        private bool hideUI()
        {
            

            bool UI1Hidden = false;
            bool UI2Hidden = false;

            point = ImageProcessing.LocateImageSingle(handler, LordsBot.Properties.Resources.UI1Opened, threshold);
            if (point.X != 0 && point.Y != 0)
            {
                MouseAction.SendMouseClick(handler, point.X, point.Y);
                Thread.Sleep(delay);
                UI1Hidden = true;
            }
            point = ImageProcessing.LocateImageSingle(handler, LordsBot.Properties.Resources.UI2Opened, threshold);
            if (point.X != 0 && point.Y != 0)
            {
                MouseAction.SendMouseClick(handler, point.X, point.Y);
                Thread.Sleep(delay);
                UI2Hidden = true;
            }


            if (UI1Hidden == true && UI2Hidden == true)
            {
                return true;
            }
            else
            {
                point = ImageProcessing.LocateImageSingle(handler, LordsBot.Properties.Resources.UI1Closed, threshold);
                if (point.X != 0 && point.Y != 0)
                {
                    UI1Hidden = true;
                }
                point = ImageProcessing.LocateImageSingle(handler, LordsBot.Properties.Resources.UI2Closed, threshold);
                if (point.X != 0 && point.Y != 0)
                {
                    UI1Hidden = true;
                }
                if (UI1Hidden == true && UI2Hidden == true)
                {
                    return true;
                }
            }


            return false;
        }
        private bool unhideUI()
        {
            

            bool UI1Hidden = false;
            bool UI2Hidden = false;

            point = ImageProcessing.LocateImageSingle(handler, LordsBot.Properties.Resources.UI1Closed, threshold);
            if (point.X != 0 && point.Y != 0)
            {
                MouseAction.SendMouseClick(handler, point.X, point.Y);
                Thread.Sleep(delay);
                UI1Hidden = true;
            }
            point = ImageProcessing.LocateImageSingle(handler, LordsBot.Properties.Resources.UI2Closed, threshold);
            if (point.X != 0 && point.Y != 0)
            {
                MouseAction.SendMouseClick(handler, point.X, point.Y);
                Thread.Sleep(delay);
                UI2Hidden = true;
            }


            if (UI1Hidden == true && UI2Hidden == true)
            {
                return true;
            }
            else
            {
                point = ImageProcessing.LocateImageSingle(handler, LordsBot.Properties.Resources.UI1Opened, threshold);
                if (point.X != 0 && point.Y != 0)
                {
                    UI1Hidden = true;
                }
                point = ImageProcessing.LocateImageSingle(handler, LordsBot.Properties.Resources.UI2Opened, threshold);
                if (point.X != 0 && point.Y != 0)
                {
                    UI1Hidden = true;
                }
                if (UI1Hidden == true && UI2Hidden == true)
                {
                    return true;
                }
            }


            return false;
        }
        private bool HideUI_Map() {
            


            point = ImageProcessing.LocateImageSingle(handler, LordsBot.Properties.Resources.UI_Map_Opened, threshold);
            if (point.X != 0 && point.Y != 0)
            {
                MouseAction.SendMouseClick(handler, point.X, point.Y);
                Thread.Sleep(delay);
                return true;
            }
            return false;
        }
        private bool UnHideUI_Map()
        {
            


            point = ImageProcessing.LocateImageSingle(handler, LordsBot.Properties.Resources.UI_Map_Closed, threshold);
            if (point.X != 0 && point.Y != 0)
            {
                MouseAction.SendMouseClick(handler, point.X, point.Y);
                Thread.Sleep(delay);
                return true;
            }
            return false;
        }
        private bool Go_To_KingdomMap() {

           


            point = ImageProcessing.LocateImageSingle(handler, LordsBot.Properties.Resources.Is_In_Turf, threshold);
            if (point.X != 0 && point.Y != 0)
            {
                MouseAction.SendMouseClick(handler, point.X, point.Y);
                Thread.Sleep(delay);
                return true;
            }
            return false;

        }
        private bool Go_To_Turf()
        {

            


            point = ImageProcessing.LocateImageSingle(handler, LordsBot.Properties.Resources.Is_In_KingdomMap, threshold);
            if (point.X != 0 && point.Y != 0)
            {
                MouseAction.SendMouseClick(handler, point.X, point.Y);
                Thread.Sleep(delay);

                worker.ReportProgress(ID, "Returning to Turf.");

                return true;
            }
            return false;

        }
        private bool Is_In_Turf() {
            point = ImageProcessing.LocateImageSingle(handler, LordsBot.Properties.Resources.Is_In_Turf, threshold);
            if (point.X != 0 && point.Y != 0)
            {
                return true;
            }
            return false;
        }

       /* private bool Return_To_Castle_Location()
        {

            


            point = ImageProcessing.LocateImageSingle(handler, LordsBot.Properties.Resources.Return_To_Castle, threshold);
            if (point.X != 0 && point.Y != 0)
            {
                MouseAction.SendMouseClick(handler, point.X, point.Y);
                Thread.Sleep(delay);
                return true;
            }
            return false;

        }*/

        private void Turf_GoToButtomRight() {

            for (int i = 0; i <= 2; i++) {
                MouseAction.SendMouseDragDown(handler, window_Width, window_Height);
                Thread.Sleep(delay/2);
            }
            for (int i = 0; i <= 4; i++)
            {
                MouseAction.SendMouseDragRight(handler, window_Width, window_Height);
                Thread.Sleep(delay/2);
            }


        }
        private void setWindowDimensions() {

        
            window_Width = Window.SizeOfWindow(handler).X;
            window_Height = Window.SizeOfWindow(handler).Y;
        }
        private void correctWindowSize()
        {
            Point winDim = Window.SizeOfWindow(handler);

            int wantDimen = 900;
            int counter;
            int errorMargin = 0;

            worker.ReportProgress(ID, "Starting Window Size Correction...");

            //Resize window twice
            for (int i = 0; i < 1; i++)
            {

                //Smaller
                counter = 0;
                Win32.SendMessage(handler, Win32.WM_LBUTTONDOWN, 0x00000001, MouseAction.MAKELPARAM(0, 0));
                Thread.Sleep(2);
                while (Window.SizeOfWindow(handler).X - errorMargin >= wantDimen)
                {

                    Win32.SendMessage(handler, Win32.WM_MOUSEMOVE, 0x0001, MouseAction.MAKELPARAM(0 + counter, 0));


                    counter++;
                    Thread.Sleep(2);
                }

                Win32.SendMessage(handler, Win32.WM_LBUTTONUP, 0x00000000, MouseAction.MAKELPARAM(0 + counter, 0));
                Thread.Sleep(1000);

                //Larger
                counter = 0;
                Win32.SendMessage(handler, Win32.WM_LBUTTONDOWN, 0x00000001, MouseAction.MAKELPARAM(0, 0));
                Thread.Sleep(2);
                while (Window.SizeOfWindow(handler).X + errorMargin <= wantDimen)
                {

                    Win32.SendMessage(handler, Win32.WM_MOUSEMOVE, 0x0001, MouseAction.MAKELPARAM(0 - counter, 0));


                    counter++;
                    Thread.Sleep(2);
                }

                Win32.SendMessage(handler, Win32.WM_LBUTTONUP, 0x00000000, MouseAction.MAKELPARAM(0 - counter, 0));
                Thread.Sleep(1000);
            }
        }
        public static int GetRandomNumber(int min, int max)
        {
            lock (syncLock)
            { // synchronize
                return getrandom.Next(min, max);
            }
        }





        public bool Account_Switch() {
            worker.ReportProgress(ID, "Starting Account Switch.");

            AccountSwitch_Tracker_Prev = AccountSwitch_Tracker;

            int errorCheck;
            bool error;
            for (int i = 0; i < 3; i++) {
                errorCheck = 0;
                error = false;

                UnHideUI_Map();
                unhideUI();
                if (Account_Switch_SettingsButton()) {
                    if (Account_Switch_AccountButton()) {
                        if (Account_Switch_SwitchAccountButton()) {
                            if (Account_Switch_SignInGoogleButton()) {
                                if (Account_Switch_SearchAccount_Manager()) {
                                    if (Account_Switch_OkButton()) {


                                        if (Account_Switch_ErrorCheck()) {
                                            error = true;
                                            errorCheck = 0;
                                            while (!isInGame()) {
                                                hitEscape();
                                                errorCheck++;
                                                if (errorCheck == 10) { break; }
                                            }
                                        }


                                        if (error == false) {
                                            if (Account_Switch_ConfirmButton()) {
                                                if (Account_Switch_CloseButton()) {


                                                    Thread.Sleep(5000);
                                                    return true;
                                                }
                                            }
                                        }
                                    
                                    }
                                }
                            }
                        }
                    }
                }

                AccountSwitch_Tracker = AccountSwitch_Tracker_Prev;
                escPopUpAll_BruteForce();
            }

            escPopUpAll_BruteForce();
            return false;
        }
        public bool Account_Switch_SettingsButton()
        {
            
                MouseAction.SendMouseClick(handler, 651, 508);
                Thread.Sleep(delay+1000);
                return true;

        }
        public bool Account_Switch_AccountButton()
        {
            
                MouseAction.SendMouseClick(handler, 267, 181);
                Thread.Sleep(delay+1000);
                return true;

        }
        public bool Account_Switch_SwitchAccountButton()
        {
           
                MouseAction.SendMouseClick(handler, 337, 392);
                Thread.Sleep(delay+1000);
                return true;

        }
        public bool Account_Switch_SignInGoogleButton()
        {
           
                MouseAction.SendMouseClick(handler, 449, 376);
                Thread.Sleep(delay*2+2500);
                return true;

        }
 
        public bool Account_Switch_SearchAccount_Manager()
        {
            //if account count = 1
            const int pos1_1x = 315; const int pos1_1y = 258;

            //if account count = 2
            const int pos1_2x = 317; const int pos1_2y = 229;
            const int pos2_2x = 313; const int pos2_2y = 284;

            //if account count = 3
            const int pos1_3x = 337; const int pos1_3y = 207;
            const int pos2_3x = 324; const int pos2_3y = 259;
            const int pos3_3x = 334; const int pos3_3y = 311;

            //if account count = 4
            const int pos1_4x = 330; const int pos1_4y = 183;
            const int pos2_4x = 325; const int pos2_4y = 232;
            const int pos3_4x = 332; const int pos3_4y = 285;
            const int pos4_4x = 322; const int pos4_4y = 336;

            //if account count = 5
            const int pos1_5x = 335; const int pos1_5y = 157;
            const int pos2_5x = 325; const int pos2_5y = 208;
            const int pos3_5x = 325; const int pos3_5y = 260;
            const int pos4_5x = 325; const int pos4_5y = 311;
            const int pos5_5x = 325; const int pos5_5y = 362;

            //if account count = 6
            const int pos1_6x = 335; const int pos1_6y = 132;
            const int pos2_6x = 325; const int pos2_6y = 182;
            const int pos3_6x = 325; const int pos3_6y = 231;
            const int pos4_6x = 325; const int pos4_6y = 284;
            const int pos5_6x = 325; const int pos5_6y = 335;
            const int pos6_6x = 325; const int pos6_6y = 388;

            //if account count = 7 or greater
            const int pos1_7x = 338; const int pos1_7y = 120;
            const int pos2_7x = 338; const int pos2_7y = 171;
            const int pos3_7x = 338; const int pos3_7y = 224;
            const int pos4_7x = 338; const int pos4_7y = 274;
            const int pos5_7x = 338; const int pos5_7y = 325;
            const int pos6_7x = 338; const int pos6_7y = 374;
            const int pos7_7x = 338; const int pos7_7y = 425;

            if (AccountSwitch_Tracker + 1 > AccountSwitch_Count)
            {
                AccountSwitch_Tracker = 1;
            }
            else
            {
                AccountSwitch_Tracker = AccountSwitch_Tracker + 1;
            }



            if (AccountSwitch_Tracker == 1) {

                if (AccountSwitch_Count == 1) {
                    MouseAction.SendMouseClick(handler, pos1_1x, pos1_1y);
                } else if (AccountSwitch_Count == 2) {
                    MouseAction.SendMouseClick(handler, pos1_2x, pos1_2y);
                }
                else if (AccountSwitch_Count == 3)
                {
                    MouseAction.SendMouseClick(handler, pos1_3x, pos1_3y);
                }
                else if (AccountSwitch_Count == 4)
                {
                    MouseAction.SendMouseClick(handler, pos1_4x, pos1_4y);
                }
                else if (AccountSwitch_Count == 5)
                {
                    MouseAction.SendMouseClick(handler, pos1_5x, pos1_5y);
                }
                else if (AccountSwitch_Count == 6)
                {
                    MouseAction.SendMouseClick(handler, pos1_6x, pos1_6y);
                }
                else 
                {
                    MouseAction.SendMouseClick(handler, pos1_7x, pos1_7y);
                }

                Thread.Sleep(delay + 1000);
                return true;
            }
            if (AccountSwitch_Tracker == 2)
            {
                if (AccountSwitch_Count == 2)
                {
                    MouseAction.SendMouseClick(handler, pos2_2x, pos2_2y);
                }
                else if (AccountSwitch_Count == 3)
                {
                    MouseAction.SendMouseClick(handler, pos2_3x, pos2_3y);
                }
                else if (AccountSwitch_Count == 4)
                {
                    MouseAction.SendMouseClick(handler, pos2_4x, pos2_4y);
                }
                else if (AccountSwitch_Count == 5)
                {
                    MouseAction.SendMouseClick(handler, pos2_5x, pos2_5y);
                }
                else if (AccountSwitch_Count == 6)
                {
                    MouseAction.SendMouseClick(handler, pos2_6x, pos2_6y);
                }
                else
                {
                    MouseAction.SendMouseClick(handler, pos2_7x, pos2_7y);
                }
                Thread.Sleep(delay + 1000);
                return true;
            }
            if (AccountSwitch_Tracker == 3)
            {
                if (AccountSwitch_Count == 3)
                {
                    MouseAction.SendMouseClick(handler, pos3_3x, pos3_3y);
                }
                else if (AccountSwitch_Count == 4)
                {
                    MouseAction.SendMouseClick(handler, pos3_4x, pos3_4y);
                }
                else if (AccountSwitch_Count == 5)
                {
                    MouseAction.SendMouseClick(handler, pos3_5x, pos3_5y);
                }
                else if (AccountSwitch_Count == 6)
                {
                    MouseAction.SendMouseClick(handler, pos3_6x, pos3_6y);
                }
                else
                {
                    MouseAction.SendMouseClick(handler, pos3_7x, pos3_7y);
                }

                Thread.Sleep(delay + 1000);
                return true;
            }
            if (AccountSwitch_Tracker == 4)
            {
                if (AccountSwitch_Count == 4)
                {
                    MouseAction.SendMouseClick(handler, pos4_4x, pos4_4y);
                }
                else if (AccountSwitch_Count == 5)
                {
                    MouseAction.SendMouseClick(handler, pos4_5x, pos4_5y);
                }
                else if (AccountSwitch_Count == 6)
                {
                    MouseAction.SendMouseClick(handler, pos4_6x, pos4_6y);
                }
                else
                {
                    MouseAction.SendMouseClick(handler, pos4_7x, pos4_7y);
                }

                Thread.Sleep(delay + 1000);
                return true;
            }
            if (AccountSwitch_Tracker == 5)
            {
                if (AccountSwitch_Count == 5)
                {
                    MouseAction.SendMouseClick(handler, pos5_5x, pos5_5y);
                }
                else if (AccountSwitch_Count == 6)
                {
                    MouseAction.SendMouseClick(handler, pos5_6x, pos5_6y);
                }
                else
                {
                    MouseAction.SendMouseClick(handler, pos5_7x, pos5_7y);
                }

                Thread.Sleep(delay + 1000);
                return true;
            }
            if (AccountSwitch_Tracker == 6)
            {
                if (AccountSwitch_Count == 6)
                {
                    MouseAction.SendMouseClick(handler, pos6_6x, pos6_6y);
                }
                else
                {
                    MouseAction.SendMouseClick(handler, pos6_7x, pos6_7y);
                }
                MouseAction.SendMouseClick(handler, pos6_7x, pos6_7y);
                Thread.Sleep(delay + 1000);
                return true;
            }
            if (AccountSwitch_Tracker == 7)
            {
                MouseAction.SendMouseClick(handler, pos7_7x, pos7_7y);
                Thread.Sleep(delay + 1000);
                return true;
            }

            if (AccountSwitch_Tracker >= 8)
            {
                for (int c = 0; c < AccountSwitch_Tracker - 7;c++) {
                    Account_Switch_SearchAccount_DragDown();
                }
                MouseAction.SendMouseClick(handler, pos7_7x, pos7_7y);
                Thread.Sleep(delay + 1000);
                return true;
            }


                


             



            return false;
        }


        public void Account_Switch_SearchAccount_DragUp() {

            MouseAction.SendMouseDragUpSmall(handler, window_Width, window_Height, 3);
            Thread.Sleep(delay);

        }
        public void Account_Switch_SearchAccount_DragDown() {
            MouseAction.SendMouseDragDownSmall_AccountSwitch(handler, window_Width, window_Height, 49);
            Thread.Sleep(delay);
        }
        public bool Account_Switch_OkButton()
        {

            const int pos1x = 636; const int pos1y = 364;
            const int pos2x = 636; const int pos2y = 390;
            const int pos3x = 636; const int pos3y = 416;
            const int pos4x = 636; const int pos4y = 441;
            const int pos5x = 636; const int pos5y = 467;
            const int pos6x = 636; const int pos6y = 492;
            const int pos7x = 627; const int pos7y = 485;

            if (AccountSwitch_Count == 1)
            {
                MouseAction.SendMouseClick(handler, pos1x, pos1y);
            }
            else if (AccountSwitch_Count == 2)
            {
                MouseAction.SendMouseClick(handler, pos2x, pos2y);
            }
            else if (AccountSwitch_Count == 3)
            {
                MouseAction.SendMouseClick(handler, pos3x, pos3y);
            }
            else if (AccountSwitch_Count == 4)
            {
                MouseAction.SendMouseClick(handler, pos4x, pos4y);
            }
            else if (AccountSwitch_Count == 5)
            {
                MouseAction.SendMouseClick(handler, pos5x, pos5y);
            }
            else if (AccountSwitch_Count == 6)
            {
                MouseAction.SendMouseClick(handler, pos6x, pos6y);
            }
            else
            {
                MouseAction.SendMouseClick(handler, pos7x, pos7y);
            }


            Thread.Sleep(delay + 2000);
            return true;

        }

        public bool Account_Switch_ConfirmButton()
        {
            const double threshold_TEMP = 0.8;
            point = ImageProcessing.LocateImageSingle(handler, LordsBot.Properties.Resources.SwitchAccount_Confirm, threshold_TEMP);
            if (point.X != 0 && point.Y != 0)
            {
                MouseAction.SendMouseClick(handler, point.X, point.Y);
                Thread.Sleep(delay*2+2000);
                return true;
            }
            return false;
        }

        public bool Account_Switch_CloseButton()
        {
            const double threshold_TEMP = 0.8;
            point = ImageProcessing.LocateImageSingle(handler, LordsBot.Properties.Resources.SwitchAccount_Close, threshold_TEMP);
            if (point.X != 0 && point.Y != 0)
            {
                MouseAction.SendMouseClick(handler, point.X, point.Y);
                Thread.Sleep(delay);
                return true;
            }
            return false;
        }

        public bool Account_Switch_ErrorCheck() {
            point = ImageProcessing.LocateImageSingle(handler, LordsBot.Properties.Resources.SwitchAccount_ErrorTest1, 0.8);
            if (point.X != 0 && point.Y != 0)
            {
                return true;
            }

            return false;

        }

        public void hitEscape() {

            SendKeyPress.SendKey(handler,SendKeyPress.VK_ESCAPE);

        }


            ~BotNox()
        {
        }

    }
}
