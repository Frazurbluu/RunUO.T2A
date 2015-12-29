namespace Server.Items
{
    public class LyricalGlasses : ElvenGlasses
	{
		public override int LabelNumber{ get{ return 1073382; } } //Lyrical Reading Glasses

		public override int InitMinHits{ get{ return 255; } }
		public override int InitMaxHits{ get{ return 255; } }

		[Constructable]
		public LyricalGlasses()
		{
			Hue = 0x47F;
		}
		public LyricalGlasses( Serial serial ) : base( serial )
		{
		}
		
		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int) 1 );
		}
		
		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();

			if ( version == 0 && Hue == 0 )
				Hue = 0x47F;
		}
	}
}
