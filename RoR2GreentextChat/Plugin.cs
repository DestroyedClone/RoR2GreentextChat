﻿using BepInEx;
using RoR2;
using System.Globalization;
using System.Security;
using System.Security.Permissions;

[module: UnverifiableCode]
#pragma warning disable CS0618 // Type or member is obsolete
[assembly: SecurityPermission(SecurityAction.RequestMinimum, SkipVerification = true)]
#pragma warning restore CS0618 // Type or member is obsolete


namespace RoR2GreentextChat
{
    [BepInPlugin("com.DestroyedClone.Greentext", "Greentext", "1.0.2")]
    public class Plugin : BaseUnityPlugin
    {
        public void Awake()
        {
            On.RoR2.Chat.UserChatMessage.ConstructChatString += UserChatMessage_ConstructChatString;
        }

        private string UserChatMessage_ConstructChatString(On.RoR2.Chat.UserChatMessage.orig_ConstructChatString orig, Chat.UserChatMessage self)
        {
            if (self.sender)
            {
                NetworkUser component = self.sender.GetComponent<NetworkUser>();
                if (component)
                {
                    if (self.text.StartsWith(">"))
                        return string.Format(CultureInfo.InvariantCulture, "{0}: <color=#789922>{1}</color>", Util.EscapeRichTextForTextMeshPro(component.userName), Util.EscapeRichTextForTextMeshPro(self.text));
                    else if (self.text.EndsWith("<"))
                        return string.Format(CultureInfo.InvariantCulture, "{0}: <color=#E0727F>{1}</color>", Util.EscapeRichTextForTextMeshPro(component.userName), Util.EscapeRichTextForTextMeshPro(self.text));
                    return orig(self);
                }
            }
            return null;
        }
    }
}
