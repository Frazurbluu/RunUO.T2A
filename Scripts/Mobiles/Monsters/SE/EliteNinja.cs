using Server.Items;

namespace Server.Mobiles
{
    public class EliteNinja : BaseCreature
	{
		public override bool ClickTitle{ get{ return false; } }

		[Constructable]
		public EliteNinja() : base( AIType.AI_Melee, FightMode.Closest, 10, 1, 0.2, 0.4 )
		{
			SpeechHue = Utility.RandomDyedHue();
			Hue = Utility.RandomSkinHue();
			Name = "an elite ninja";

			Body = ( this.Female = Utility.RandomBool() ) ? 0x191 : 0x190;

			SetHits( 251, 350 );

			SetStr( 126, 225 );
			SetDex( 81, 95 );
			SetInt( 151, 165 );

			SetDamage( 12, 20 );

			SetSkill( SkillName.Anatomy, 105.0, 120.0 );
			SetSkill( SkillName.MagicResist, 80.0, 100.0 );
			SetSkill( SkillName.Tactics, 115.0, 130.0 );
			SetSkill( SkillName.Wrestling, 95.0, 120.0 );
			SetSkill( SkillName.Fencing, 95.0, 120.0 );
			SetSkill( SkillName.Macing, 95.0, 120.0 );
			SetSkill( SkillName.Swords, 95.0, 120.0 );


			Fame = 8500;
			Karma = -8500;

			/* TODO:	
					Uses Smokebombs
					Hides
					Stealths
					Can use Ninjitsu Abilities
					Can change weapons during a fight
			*/
					


			Utility.AssignRandomHair( this );
		}

		public override bool BardImmune{ get{ return true; } }

		public override void GenerateLoot()
		{
			AddLoot( LootPack.FilthyRich );
			AddLoot( LootPack.Rich );
			AddLoot( LootPack.Gems, 2 );
		}
		
		public override bool AlwaysMurderer{ get{ return true; } }

		public EliteNinja( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 0 ); // version
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();
		}
	}
}