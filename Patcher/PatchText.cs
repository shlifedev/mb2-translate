using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Patcher
{
    public static class PatchText
    {

        public static string SANDBOX_SUB_XML = @"<?xml version=""1.0"" encoding=""utf-8""?>
<Module>
<Name value = ""Sandbox""/>
<Id value = ""Sandbox""/>
	<Version value = ""e1.0.2""/>
    <DefaultModule value=""true""/>
	<SingleplayerModule value=""true""/>
  <Official value =""true"" />
  <DependedModules>
		<DependedModule Id=""Native""/>
        <DependedModule Id=""SandBoxCore""/>
		<DependedModule Id=""MBKoreanFont""/>
    </DependedModules>
    <SubModules>
        <SubModule>
            <Name value = ""SandBox""/>
			<DLLName value = ""SandBox.dll""/>

            <SubModuleClassType value = ""SandBox.SandBoxSubModule""/>			
			<Tags>
				<Tag key=""DedicatedServerType"" value =""none"" />
				<Tag key=""IsNoRenderModeElement"" value =""false"" />
			</Tags>
		</SubModule>
		<SubModule>
			<Name value = ""SandBox.View""/>

            <DLLName value = ""SandBox.View.dll""/>
			<Assemblies>
				<Assembly value=""SandBox.ViewModelCollection.dll""/>

            </Assemblies>

            <SubModuleClassType value = ""SandBox.View.SandBoxViewSubModule""/>
			<Tags>
				<Tag key=""DedicatedServerType"" value =""none"" />
				<Tag key=""IsNoRenderModeElement"" value =""false"" />
			</Tags>
		</SubModule>		
		<SubModule>
			<Name value = ""SandBox.GauntletUI""/>

            <DLLName value = ""SandBox.GauntletUI.dll""/>
			<SubModuleClassType value = ""SandBox.GauntletUI.SandBoxGauntletUISubModule""/>

            <Tags>

                <Tag key=""DedicatedServerType"" value =""none"" />
				<Tag key=""IsNoRenderModeElement"" value =""false"" />
			</Tags>
		</SubModule>
	</SubModules>
	
	<Xmls>
		<XmlNode>                
			<XmlName id=""Items"" path=""spitems""/>
			<IncludedGameTypes>
				<GameType value = ""Campaign""/>

                <GameType value = ""CampaignStoryMode""/>
			</IncludedGameTypes>
		</XmlNode>  
		<XmlNode>                
			<XmlName id=""SPCultures"" path=""spcultures""/>
			<IncludedGameTypes>
				<GameType value = ""Campaign""/>

                <GameType value = ""CampaignStoryMode""/>
			</IncludedGameTypes>
		</XmlNode>             
		<XmlNode>                
			<XmlName id=""NPCCharacters"" path=""spnpccharacters""/>
			<IncludedGameTypes>
				<GameType value = ""Campaign""/>

                <GameType value = ""CampaignStoryMode""/>
			</IncludedGameTypes>
		</XmlNode>           
		<XmlNode>
			<XmlName id=""partyTemplates"" path=""partyTemplates""/>
			<IncludedGameTypes>
				<GameType value = ""Campaign""/>

                <GameType value = ""CampaignStoryMode""/>
			</IncludedGameTypes>
		</XmlNode>	
		<XmlNode>               
			<XmlName id=""NPCCharacters"" path=""lords""/>
			<IncludedGameTypes>
				<GameType value = ""Campaign""/>

                <GameType value = ""CampaignStoryMode""/>
			</IncludedGameTypes>
		</XmlNode>               
		<XmlNode>                
			<XmlName id=""NPCCharacters"" path=""companions""/>
			<IncludedGameTypes>
				<GameType value = ""Campaign""/>

                <GameType value = ""CampaignStoryMode""/>
			</IncludedGameTypes>
		</XmlNode>               
		<XmlNode>                
			<XmlName id=""NPCCharacters"" path=""bandits""/>
			<IncludedGameTypes>
				<GameType value = ""Campaign""/>

                <GameType value = ""CampaignStoryMode""/>
			</IncludedGameTypes>
		</XmlNode> 
		<XmlNode>                
			<XmlName id=""Heroes"" path=""heroes""/>
			<IncludedGameTypes>
				<GameType value = ""Campaign""/>

                <GameType value = ""CampaignStoryMode""/>
			</IncludedGameTypes>
		</XmlNode>               
		<XmlNode>                
			<XmlName id=""NPCCharacters"" path=""caravans""/>
			<IncludedGameTypes>
				<GameType value = ""Campaign""/>

                <GameType value = ""CampaignStoryMode""/>
			</IncludedGameTypes>
		</XmlNode>                       
		<XmlNode>                
			<XmlName id=""NPCCharacters"" path=""spspecialcharacters""/>
			<IncludedGameTypes>
				<GameType value = ""Campaign""/>

                <GameType value = ""CampaignStoryMode""/>
			</IncludedGameTypes>
		</XmlNode>               
		<XmlNode>                
			<XmlName id=""Kingdoms"" path=""spkingdoms""/>
			<IncludedGameTypes>
				<GameType value = ""Campaign""/>

                <GameType value = ""CampaignStoryMode""/>
			</IncludedGameTypes>
		</XmlNode>               
		<XmlNode>                
			<XmlName id=""Factions"" path=""spclans""/>
			<IncludedGameTypes>
				<GameType value = ""Campaign""/>

                <GameType value = ""CampaignStoryMode""/>
			</IncludedGameTypes>
		</XmlNode>               
		<XmlNode>                
			<XmlName id=""WorkshopTypes"" path=""spworkshops""/>
			<IncludedGameTypes>
				<GameType value = ""Campaign""/>

                <GameType value = ""CampaignStoryMode""/>
			</IncludedGameTypes>
		</XmlNode>               
		<XmlNode>                
			<XmlName id=""LocationComplexTemplates"" path=""location_complex_templates""/>
			<IncludedGameTypes>
				<GameType value = ""Campaign""/>

                <GameType value = ""CampaignStoryMode""/>
			</IncludedGameTypes>
		</XmlNode>               
		<XmlNode>                
			<XmlName id=""Concepts"" path=""concept_strings""/>
			<IncludedGameTypes>
				<GameType value = ""Campaign""/>

                <GameType value = ""CampaignStoryMode""/>
			</IncludedGameTypes>
		</XmlNode>               
		<XmlNode>                
			<XmlName id=""Settlements"" path=""settlements""/>
			<IncludedGameTypes>
				<GameType value = ""Campaign""/>

                <GameType value = ""CampaignStoryMode""/>
			</IncludedGameTypes>
		</XmlNode>
	</Xmls>
</Module>
";
        public static string LANGUAGE_XML = @"<!-- Korean Patch by 마앤블 공식카페 https://cafe.naver.com/warband -->
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
