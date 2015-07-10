using System;
using Phidget21COM;
using Phidgets;
using Phidgets.Events;

namespace BDPPMaster.Models
{
    public static class PhidgetThing
    {
        public static void doPhidgetThings()
        {
            try
            {
                RFID rfid = new RFID(); //Declare an RFID object
                //initialize our Phidgets RFID reader and hook the event handlers
                rfid.Attach += new AttachEventHandler(rfid_Attach);
                rfid.Detach += new DetachEventHandler(rfid_Detach);
                rfid.Error += new ErrorEventHandler(rfid_Error);

                rfid.Tag += new TagEventHandler(rfid_Tag);
                rfid.TagLost += new TagEventHandler(rfid_TagLost);
                rfid.open();
                
        
                
                //Wait for a Phidget RFID to be attached before doing anything with 
                //the object
                rfid.waitForAttachment();

                ////turn on the antenna and the led to show everything is working
                rfid.Antenna = true;
                rfid.LED = true;

                //turn off the led
                rfid.LED = false;

                //close the phidget and dispose of the object
                rfid.close();
                rfid = null;

            }
            catch (PhidgetException ex)
            {
                Console.WriteLine(ex.Description);
            }
        }

        //attach event handler...display the serial number of the attached RFID phidget
        static void rfid_Attach(object sender, AttachEventArgs e)
        {
            Console.WriteLine("RFIDReader {0} attached!",
                                    e.Device.SerialNumber.ToString());
        }

        //detach event handler...display the serial number of the detached RFID phidget
        static void rfid_Detach(object sender, DetachEventArgs e)
        {
            Console.WriteLine("RFID reader {0} detached!",
                                    e.Device.SerialNumber.ToString());
        }

        //Error event handler...display the error description string
        static void rfid_Error(object sender, ErrorEventArgs e)
        {
            Console.WriteLine(e.Description);
        }

        //Print the tag code of the scanned tag
        static void rfid_Tag(object sender, TagEventArgs e)
        {
            Console.WriteLine("Tag {0} scanned", e.Tag);
        }

        //print the tag code for the tag that was just lost
        static void rfid_TagLost(object sender, TagEventArgs e)
        {
            Console.WriteLine("Tag {0} lost", e.Tag);
        }

    }
}