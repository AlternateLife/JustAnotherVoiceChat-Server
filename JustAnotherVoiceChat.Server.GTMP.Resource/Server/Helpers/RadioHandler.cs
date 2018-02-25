/*
 * File: RadioHandler.cs
 * Date: 24.2.2018,
 *
 * MIT License
 *
 * Copyright (c) 2018 JustAnotherVoiceChat
 *
 * Permission is hereby granted, free of charge, to any person obtaining a copy
 * of this software and associated documentation files (the "Software"), to deal
 * in the Software without restriction, including without limitation the rights
 * to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
 * copies of the Software, and to permit persons to whom the Software is
 * furnished to do so, subject to the following conditions:
 *
 * The above copyright notice and this permission notice shall be included in all
 * copies or substantial portions of the Software.
 *
 * THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
 * IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
 * FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
 * AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
 * LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
 * OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
 * SOFTWARE.
 */

using System;
using GrandTheftMultiplayer.Server.API;
using GrandTheftMultiplayer.Server.Elements;
using GrandTheftMultiplayer.Shared.Math;
using JustAnotherVoiceChat.Server.GTMP.Extensions;

namespace JustAnotherVoiceChat.Server.GTMP.Resource.Helpers
{
    public class RadioHandler : Script
    {

        private const string RadioStatus = "RADIO_ACTIVE";
        private const string RadioChannel = "RADIO_CHANNEL";

        public bool EnableRadio(Client sender, string channel)
        {
            if (sender == null)
            {
                throw new ArgumentNullException(nameof(sender));
            }

            if (channel == null)
            {
                throw new ArgumentNullException(nameof(channel));
            }

            sender.setData(RadioStatus, true);
            sender.setData(RadioChannel, channel);

            return true;
        }

        public bool DisableRadio(Client sender)
        {
            if (sender == null)
            {
                throw new ArgumentNullException(nameof(sender));
            }

            sender.setData(RadioStatus, false);
            sender.setData(RadioChannel, "Off");

            return true;
        }

        public bool UseRadio(Client sender, string channel, bool decision)
        {
            if (sender == null)
            {
                throw new ArgumentNullException(nameof(sender));
            }

            if (channel == null)
            {
                throw new ArgumentNullException(nameof(channel));
            }

            if (channel == "Off")
            {
                return false;
            }

            if (decision == true)
            {
                foreach (var reciever in API.getAllPlayers())
                {
                    if (reciever == null || reciever.IsNull)
                    {
                        continue;
                    }

                    if (reciever.getData(RadioChannel) == channel && reciever.getData(RadioStatus) == true)
                    {
                        reciever.SetRelativeSpeakerPosition(sender, new Vector3(1, 0, 0));
                    }
                }
            }
            else
            {
                foreach (var reciever in API.getAllPlayers())
                {
                    if (reciever == null || reciever.IsNull)
                    {
                        continue;
                    }

                    if (reciever.getData(RadioChannel) == channel && reciever.getData(RadioStatus) == true || reciever.hasData(RadioStatus) == false)
                    {
                        reciever.ResetRelativeSpeakerPosition(sender);
                    }
                }
            }

            return true;
        }
    }
}
