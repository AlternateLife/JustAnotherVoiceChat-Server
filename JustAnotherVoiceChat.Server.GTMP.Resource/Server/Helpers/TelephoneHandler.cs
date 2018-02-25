/*
 * File: TelephoneHandler.cs
 * Date: 22.2.2018,
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
using GrandTheftMultiplayer.Server.Elements;
using JustAnotherVoiceChat.Server.GTMP.Extensions;
using JustAnotherVoiceChat.Server.GTMP.Interfaces;
using JustAnotherVoiceChat.Server.Wrapper.Math;

namespace JustAnotherVoiceChat.Server.GTMP.Resource.Helpers
{
    public class TelephoneHandler
    {
        private readonly IGtmpVoiceServer _server;

        private const string DataCallStatus = "CALL_ACTIVE";
        private const string DataCallOpponent = "CALL_OPPONENT";
        private const string DataCallIsCaller = "CALL_CALLER";

        public TelephoneHandler(IGtmpVoiceServer server)
        {
            _server = server;
        }

        public bool StartCall(Client caller, Client callee)
        {
            if (caller == null)
            {
                throw new ArgumentNullException(nameof(caller));
            }
            
            if (callee == null)
            {
                throw new ArgumentNullException(nameof(callee));
            }

            if (caller.hasData(DataCallOpponent) || callee.hasData(DataCallOpponent))
            {
                return false;
            }

            caller.setData(DataCallOpponent, callee);
            caller.setData(DataCallStatus, false);
            caller.setData(DataCallIsCaller, true);
            
            callee.setData(DataCallOpponent, caller);
            callee.setData(DataCallStatus, false);
            
            return true;
        }

        public bool AnswerCall(Client callee, bool decision, out Client caller)
        {
            if (!callee.hasData(DataCallOpponent) || callee.hasData(DataCallIsCaller) || callee.hasData(DataCallStatus) && (bool) callee.getData(DataCallStatus))
            {
                caller = null;
                return false;
            }

            caller = (Client) callee.getData(DataCallOpponent);

            if (decision)
            {
                callee.setData(DataCallStatus, true);
                caller.setData(DataCallStatus, true);

                var voiceClientCaller = caller.GetVoiceClient();
                var voiceClientCallee = callee.GetVoiceClient();

                voiceClientCallee.SetRelativeSpeakerPosition(voiceClientCaller, new Vector3(1, 0, 0));
                voiceClientCaller.SetRelativeSpeakerPosition(voiceClientCallee, new Vector3(1, 0, 0));
            }
            else
            {
                callee.resetData(DataCallOpponent);
                callee.resetData(DataCallStatus);
                
                caller.resetData(DataCallOpponent);
                caller.resetData(DataCallStatus);
            }

            return true;
        }

        public bool HangupCall(Client player, out Client opponent)
        {
            if (!player.hasData(DataCallOpponent) || !player.hasData(DataCallStatus) || !(bool) player.getData(DataCallStatus))
            {
                opponent = null;
                return false;
            }

            opponent = (Client) player.getData(DataCallOpponent);

            var voiceClient = player.GetVoiceClient();
            var voiceClientOpponent = opponent.GetVoiceClient();
            
            player.resetData(DataCallOpponent);
            player.resetData(DataCallStatus);
            player.resetData(DataCallIsCaller);

            voiceClient.ResetRelativeSpeakerPosition(voiceClientOpponent);
                
            opponent.resetData(DataCallOpponent);
            opponent.resetData(DataCallStatus);
            opponent.resetData(DataCallIsCaller);
            
            voiceClientOpponent.ResetRelativeSpeakerPosition(voiceClient);
            
            return true;
        }
        
    }
}
