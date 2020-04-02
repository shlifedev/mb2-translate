using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Patcher
{
    public static class PatchText
    {
        public static string LANGUAGE_XML = @"
<Languages DefaultLanguage=""English"">
  <!--Maps English fonts to Foreign fonts to use on language change--> 
  <!-- English -->
  <Language id=""English"" DefaultFont=""Galahad"">
    <Map From=""FiraSansExtraCondensed-Light"" To=""FiraSansExtraCondensed-Light""/>
    <Map From=""FiraSansExtraCondensed-Medium"" To=""FiraSansExtraCondensed-Medium""/>
    <Map From=""FiraSansExtraCondensed-Regular"" To=""FiraSansExtraCondensed-Regular""/>
    <Map From=""Galahad"" To=""Galahad""/>
    <Map From=""Galahad_Numbers_Bold"" To=""Galahad_Numbers_Bold""/>
  </Language> 
  <!-- Türkçe -->
  <Language id=""Türkçe"" DefaultFont=""Galahad"">
    <Map From=""FiraSansExtraCondensed-Light"" To=""FiraSansExtraCondensed-Light""/>
    <Map From=""FiraSansExtraCondensed-Medium"" To=""FiraSansExtraCondensed-Medium""/>
    <Map From=""FiraSansExtraCondensed-Regular"" To=""FiraSansExtraCondensed-Regular""/>
    <Map From=""Galahad"" To=""Galahad""/>
    <Map From=""Galahad_Numbers_Bold"" To=""Galahad_Numbers_Bold""/>
  </Language> 
  <!-- Chinese -->
  <Language id=""繁體中文"" DefaultFont=""simkai"">
    <Map From=""FiraSansExtraCondensed-Light"" To=""simkai""/>
    <Map From=""FiraSansExtraCondensed-Medium"" To=""simkai""/>
    <Map From=""FiraSansExtraCondensed-Regular"" To=""simkai""/>
    <Map From=""Galahad"" To=""simkai""/>
    <Map From=""Galahad_Numbers_Bold"" To=""simkai""/>
  </Language>
  <Language id=""简体中文"" DefaultFont=""simkai"">
    <Map From=""FiraSansExtraCondensed-Light"" To=""simkai""/>
    <Map From=""FiraSansExtraCondensed-Medium"" To=""simkai""/>
    <Map From=""FiraSansExtraCondensed-Regular"" To=""simkai""/>
    <Map From=""Galahad"" To=""simkai""/>
    <Map From=""Galahad_Numbers_Bold"" To=""simkai""/>
  </Language> 
  <!-- Japanese -->
  <Language id=""日本語"" DefaultFont=""simkai""/> 
  <!-- Korean -->
  <Language id=""한국어"" DefaultFont=""simkai"">
     <Map From=""FiraSansExtraCondensed-Light"" To=""simkai""/>
    <Map From=""FiraSansExtraCondensed-Medium"" To=""simkai""/>
    <Map From=""FiraSansExtraCondensed-Regular"" To=""simkai""/>
    <Map From=""Galahad"" To=""simkai""/>
    <Map From=""Galahad_Numbers_Bold"" To=""simkai""/>
   </Language> 
  <!-- French -->
  <Language id=""Français"" DefaultFont=""FiraSansExtraCondensed-Regular""/>
</Languages> 
    ";
    }
}
