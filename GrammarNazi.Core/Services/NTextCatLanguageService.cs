﻿using GrammarNazi.Core.Utilities;
using GrammarNazi.Domain.Entities;
using GrammarNazi.Domain.Services;
using NTextCat;
using System;
using System.Linq;

namespace GrammarNazi.Core.Services
{
    public class NTextCatLanguageService : ILanguageService
    {
        private readonly RankedLanguageIdentifierFactory _rankedLanguageIdentifierFactory;

        public NTextCatLanguageService()
        {
            _rankedLanguageIdentifierFactory = new RankedLanguageIdentifierFactory();
        }

        public LanguageInformation IdentifyLanguage(string text)
        {
            var identifier = _rankedLanguageIdentifierFactory.Load("Library/Core14.profile.xml");
            var languages = identifier.Identify(text).Where(v => LanguageUtils.GetSupportedLanguages().Contains(v.Item1.Iso639_3));

            if (!languages.Any())
                return default;

            var (languageInfo, _) = languages.First();

            return new LanguageInformation
            {
                ThreeLetterISOLanguageName = languageInfo.Iso639_3,
                TwoLetterISOLanguageName = LanguageUtils.GetLanguageCode(languageInfo.Iso639_3)
            };
        }
    }
}