using UnityEngine;
using System.Collections.Generic;
using ShortLegStudio.Enchilada;
using ShortLegStudio.RPG.Characters;
using ShortLegStudio;
using ShortLegStudio.Dice;
using YamlDotNet.RepresentationModel;

public class ClassYamlGateway : EntityGateway<Class> {
	//Static Values
	const string CLASS_DATA_FILE = "Data/classes.yml";
	private IList<Class> _Classes;

	public ClassYamlGateway(YamlNodeWrapper yaml) {
		_Classes = LoadFromYaml(yaml);
	}

	public ClassYamlGateway() {
		_Classes = LoadFromYaml(FileHelper.OpenYaml(CLASS_DATA_FILE));
	}

	public IEnumerable<Class> All() {
		return _Classes;
	}

	private static IList<Class> LoadFromYaml(YamlNodeWrapper yaml) {
		var classes = new List<Class> ();

		foreach (var node in yaml.Children()) {
			var cls = new Class ();
			cls.Name = node.GetString ("name"); 
			ShortLog.Debug ("Loading Class: " + cls.Name);
			cls.SkillPoints = node.GetInteger ("skillpoints");
			cls.HitDice = DiceStrings.ParseSides (node.GetString ("hitdice"));
			cls.BaseAttackBonusRate = node.GetFloat ("baseattackbonus");
			cls.FortitudeSaveRate = node.GetFloat ("fortitude");
			cls.ReflexSaveRate = node.GetFloat ("reflex");
			cls.WillSaveRate = node.GetFloat ("will");

			var armor = node.GetCommaStringOptional ("armorproficiencies");
			foreach (var a in armor) {
				cls.ArmorProficiencies.Add (
					string.Format ("Armor Proficiency ({0})", a.Capitalize ())
				);
			}

			var weapons = node.GetCommaStringOptional("weaponproficiencies");
			cls.WeaponProficiencies.Add(weapons);

			//Get the Skills for this class
			var skills = node.GetNode ("skills").Children();
			foreach (var s in skills) {
				cls.AddClassSkill (s.Value);
			}

			classes.Add (cls);
		}

		return classes;
	}
}
