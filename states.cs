using System;

namespace Example_07.States
{
    public enum DeviceType
    {
        Wifi,
        Usb
    }

    public interface ICopyMakerState
    {
        void GetChange      (CopyMaker context);
        void PrintDocument  (CopyMaker context);
        void ChooseDocument (CopyMaker context, int documentId);
        void ChooseDevice   (CopyMaker context, DeviceType device);
        void PutMoney       (CopyMaker context, uint cash);
    }

    public class CopyMaker
    {
        private uint Cash { get; set; }
        private DeviceType DeviceType { get; set; }
        private int DocumentId { get; set; }

        public ICopyMakerState State { get; set; }

        public CopyMaker() {
            State = new PendingMoneyState();
        }

        public void PutMoney(uint cash) {
            Cash += cash;
        }

        public void ChooseDevice(DeviceType device) {
            DeviceType = device;
        }

        public void ChooseDocument(int documentId) {
            DocumentId = documentId;
        }

        public void PrintDocument() {
            if (Cash < 2)
            {
                throw new Exception("You have no enough money for this operation");
            }

            Cash -= 2;
        }

        public uint GetChange() {
            var change = Cash;
            Cash = 0;
            return change;
        }
    }

    public class PendingMoneyState : ICopyMakerState
    {
        public void GetChange(CopyMaker context) {
            throw new Exception("You didn't give me any money");
        }

        public void PrintDocument(CopyMaker context) {
            throw new Exception("You didn't give me any money");
        }

        public void ChooseDocument(CopyMaker context, int documentId) {
            throw new Exception("You didn't give me any money");
        }

        public void ChooseDevice(CopyMaker context, DeviceType device) {
            throw new Exception("You didn't give me any money");
        }

        public void PutMoney(CopyMaker context, uint cash) {
            context.PutMoney(cash);
            context.State = new PendingDeviceChoiceState();
        }
    }

    public class PendingDeviceChoiceState : ICopyMakerState
    {
        public void GetChange(CopyMaker context) {
            context.GetChange();
            context.State = new FinalState();
        }

        public void PrintDocument(CopyMaker context) {
            throw new Exception("Choose a device type please");
        }

        public void ChooseDocument(CopyMaker context, int documentId) {
            throw new Exception("Choose a device type please");
        }

        public void ChooseDevice(CopyMaker context, DeviceType device) {
            context.ChooseDevice(device);
            context.State = new PendingDocumentChoiceState();
        }

        public void PutMoney(CopyMaker context, uint cash) {
            context.PutMoney(cash);
            context.State = new PendingDeviceChoiceState();
        }
    }

    public class PendingDocumentChoiceState : ICopyMakerState
    {
        public void GetChange(CopyMaker context) {
            context.GetChange();
            context.State = new FinalState();
        }

        public void PrintDocument(CopyMaker context) {
            throw new Exception("Choose a document please");
        }

        public void ChooseDocument(CopyMaker context, int documentId) {
            context.ChooseDocument(documentId);
            context.State = new PendingPrintingState();
        }

        public void ChooseDevice(CopyMaker context, DeviceType device) {
            context.ChooseDevice(device);
            context.State = new PendingDocumentChoiceState();
        }

        public void PutMoney(CopyMaker context, uint cash) {
            context.PutMoney(cash);
            context.State = new PendingDocumentChoiceState();
        }
    }

    public class PendingPrintingState : ICopyMakerState
    {
        public void GetChange(CopyMaker context) {
            context.State = new FinalState();
        }

        public void PrintDocument(CopyMaker context) {
            context.PrintDocument();
            context.State = new PendingDocumentChoiceState();
        }

        public void ChooseDocument(CopyMaker context, int documentId) {
            context.ChooseDocument(documentId);
            context.State = new PendingPrintingState();
        }

        public void ChooseDevice(CopyMaker context, DeviceType device) {
            context.ChooseDevice(device);
            context.State = new PendingDocumentChoiceState();
        }

        public void PutMoney(CopyMaker context, uint cash) {
            context.PutMoney(cash);
            context.State = new PendingPrintingState();
        }
    }

    public class FinalState : ICopyMakerState
    {
        public void GetChange(CopyMaker context) {
            throw new Exception("I already gave you the money");
        }

        public void PrintDocument(CopyMaker context) {
            throw new Exception("I cannot work without money");
        }

        public void ChooseDocument(CopyMaker context, int documentId) {
            throw new Exception("I cannot work without money");
        }

        public void ChooseDevice(CopyMaker context, DeviceType device) {
            throw new Exception("I cannot work without money");
        }

        public void PutMoney(CopyMaker context, uint cash) {
            context.PutMoney(cash);
            context.State = new PendingDeviceChoiceState();
        }
    }
}
