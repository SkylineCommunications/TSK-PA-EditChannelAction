/*
****************************************************************************
*  Copyright (c) 2023,  Skyline Communications NV  All Rights Reserved.    *
****************************************************************************

By using this script, you expressly agree with the usage terms and
conditions set out below.
This script and all related materials are protected by copyrights and
other intellectual property rights that exclusively belong
to Skyline Communications.

A user license granted for this script is strictly for personal use only.
This script may not be used in any way by anyone without the prior
written consent of Skyline Communications. Any sublicensing of this
script is forbidden.

Any modifications to this script by the user are only allowed for
personal use and within the intended purpose of the script,
and will remain the sole responsibility of the user.
Skyline Communications will not be responsible for any damages or
malfunctions whatsoever of the script resulting from a modification
or adaptation by the user.

The content of this script is confidential information.
The user hereby agrees to keep this confidential information strictly
secret and confidential and not to disclose or reveal it, in whole
or in part, directly or indirectly to any person, entity, organization
or administration without the prior written consent of
Skyline Communications.

Any inquiries can be addressed to:

	Skyline Communications NV
	Ambachtenstraat 33
	B-8870 Izegem
	Belgium
	Tel.	: +32 51 31 35 69
	Fax.	: +32 51 31 01 29
	E-mail	: info@skyline.be
	Web		: www.skyline.be
	Contact	: Ben Vandenberghe

****************************************************************************
Revision History:

DATE		VERSION		AUTHOR			COMMENTS

08/12/2023	1.0.0.1		JWO, Skyline	Initial version
****************************************************************************
*/

namespace TSK_PA_EditChannelAction_1
{
	using System;
	using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;
    using System.Text;
	using Skyline.DataMiner.Automation;
    using Skyline.DataMiner.Net.Apps.DataMinerObjectModel;
    using Skyline.DataMiner.Net.Apps.DataMinerObjectModel.Actions;
    using Skyline.DataMiner.Net.LogHelpers;
    using Skyline.DataMiner.Net.History;
    using Skyline.DataMiner.Net.Messages;
    using Skyline.DataMiner.Net.Messages.SLDataGateway;
    using Skyline.DataMiner.Net.Profiles;
    using Skyline.DataMiner.Net.ResourceManager.Objects;
    using Skyline.DataMiner.Net.Sections;
    using Skyline.DataMiner.DataMinerSolutions.ProcessAutomation.Objects;
    using Skyline.DataMiner.DataMinerSolutions.ProcessAutomation.MessageHandler;

    /// <summary>
    /// Represents a DataMiner Automation script.
    /// </summary>
    public class Script
    {
        private Engine _engine;

        /// <summary>
        /// The Script entry point.
        /// </summary>
        /// <param name="engine">Link with SLAutomation process.</param>
        public void Run(Engine engine)
        {

            string guids = engine.GetScriptParam("guids").Value;
            engine.GenerateInformation("guids: " + guids);


            string processName = "Edit Channel";


            //string processName = "Create Channels";
            var domHelper = new DomHelper(engine.SendSLNetMessages, "process_automation");
            //var domInstanceFilter = DomInstanceExposers.Id.Equal(Guid.Parse(GUID));
            List<Guid> allGuids = ParseGuidList(guids);

            foreach (Guid myGuid in allGuids)
            {
                var domInstanceFilter = DomInstanceExposers.Id.Equal(myGuid);
                var instances = domHelper.DomInstances.Read(domInstanceFilter).First();
                ProcessHelper.PushToken(processName, "Jeroen", instances.ID);
                //ProcessHelper.PushToken(processName, new PushPaTokenSettings {maxTimInQueue = 240, BusinessKey = "Jeroen",DomInstanceId = instances.ID});
            }
        }

        private List<Guid> ParseGuidList(string guids)
        {
            guids = guids.Replace("[", "");
            guids = guids.Replace("]", "");
            guids = guids.Replace("\"", "");
            return Array.ConvertAll(guids.Split(','), s => new Guid(s)).ToList();
        }
    }
}