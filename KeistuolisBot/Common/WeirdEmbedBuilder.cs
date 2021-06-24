using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Discord;

namespace KeistuolisBot.Common
{

    public enum EmbedStyle
    {
        Error, //Red
        Success, //Green
        Warning, //Yellow
        Information, //Blue
    }

    public class WeirdEmbedBuilder : EmbedBuilder
    {
        #region Colors
        private readonly Color _errorColor = new (244, 67, 54);
        private readonly Color _successColor = new (76, 175, 80);
        private readonly Color _warningColor = new (255, 152, 0);
        private readonly Color _informationColor = new (3, 169, 244);
        #endregion
        public EmbedStyle Style { get; set; }


        public WeirdEmbedBuilder()
        {
            Style = EmbedStyle.Information;
        }


        #region Color setters

        public EmbedBuilder WithError()
        {
            Style = EmbedStyle.Error;
            return this;
        }

        public EmbedBuilder WithSuccess()
        {
            Style = EmbedStyle.Success;
            return this;
        }

        public EmbedBuilder WithWarning()
        {
            Style = EmbedStyle.Warning;
            return this;
        }

        public EmbedBuilder WithInformation()
        {
            Style = EmbedStyle.Information;
            return this;
        }

        #endregion
        public Embed Build()
        {

            //if (string.IsNullOrEmpty(Title)) throw new InvalidOperationException("Title cant be empty");

            Color color = Style switch
            {
                EmbedStyle.Error => _errorColor,
                EmbedStyle.Success => _successColor,
                EmbedStyle.Warning => _warningColor,
                EmbedStyle.Information => _informationColor,
                _ => throw new ArgumentOutOfRangeException()
            };

            WithColor(color);
            WithFooter("Keistuolis");
            WithCurrentTimestamp();

            return base.Build();
        }
    }
}
