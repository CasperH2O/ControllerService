﻿using System.Collections.Generic;
using System.Numerics;
using ControllerCommon.Inputs;
using ControllerCommon.Managers;
using WindowsInput.Events;

namespace ControllerCommon.Devices;

public class AOKZOEA1Pro : AOKZOEA1
{
    public AOKZOEA1Pro()
    {
        // device specific settings
        ProductIllustration = "device_aokzoe_a1";
        ProductModel = "AOKZOEA1Pro";

        // https://www.amd.com/en/products/apu/amd-ryzen-7-7840u
        nTDP = new double[] { 15, 15, 20 };
        cTDP = new double[] { 4, 28 };
        GfxClock = new double[] { 100, 2700 };
    }

    public override bool Open()
    {
        var success = base.Open();
        if (!success)
            return false;

        // allow AOKZOE A1 Pro button to pass key inputs for Turbo button
        LogManager.LogInformation("Unlocked {0} OEM button", ButtonFlags.OEM3);
        return ECRamDirectWrite(0xF1, ECDetails, 0x40);
    }

    public override void Close()
    {
        LogManager.LogInformation("Locked {0} OEM button", ButtonFlags.OEM3);
        ECRamDirectWrite(0xF1, ECDetails, 0x00);
        base.Close();
    }
}