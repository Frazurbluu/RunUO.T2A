﻿namespace Server.Items
{
    public class EarringBoxSet : RedVelvetGiftBox
	{
		[Constructable]
		public EarringBoxSet() : base()
		{
		}

		public EarringBoxSet( Serial serial )
			: base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int)0 ); // version
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();
		}
	}

	public class EarringsOfProtection : BaseJewel
	{
		[Constructable]
		public EarringsOfProtection() : base( 0x1087, Layer.Earrings )
		{
			LootType = LootType.Blessed;
		}

		public EarringsOfProtection( Serial serial )
			: base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int)0 ); // version
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();
		}
	}
}