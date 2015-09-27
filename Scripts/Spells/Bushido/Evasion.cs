using System;
using System.Collections;
using Server.Items;

namespace Server.Spells.Bushido
{
    public class Evasion : SamuraiSpell
	{
		private static SpellInfo m_Info = new SpellInfo(
				"Evasion", null,
				-1,
				9002
			);

		public override TimeSpan CastDelayBase { get { return TimeSpan.FromSeconds( 0.25 ); } }

		public override double RequiredSkill { get { return 60.0; } }
		public override int RequiredMana { get { return 10; } }

		public override bool CheckCast()
		{
			if( VerifyCast( Caster, true ) )
				return base.CheckCast();

			return false;
		}

		public static bool VerifyCast( Mobile Caster, bool messages )
		{
			if( Caster == null ) // Sanity
				return false;

			BaseWeapon weap = Caster.FindItemOnLayer( Layer.OneHanded ) as BaseWeapon;

            if (weap == null)
                weap = Caster.FindItemOnLayer(Layer.TwoHanded) as BaseWeapon;

            if (!(Caster.FindItemOnLayer(Layer.TwoHanded) is BaseShield))
            {
                if (messages)
                {
                    Caster.SendLocalizedMessage(1062944); // You must have a weapon or a shield equipped to use this ability!
                }
                return false;
            }

            if ( !Caster.CanBeginAction( typeof( Evasion ) ) ) {
				if ( messages ) {
					Caster.SendLocalizedMessage( 501789 ); // You must wait before trying again.
				}
				return false;
			}

			return true;
		}

		public static bool CheckSpellEvasion( Mobile defender )
		{
			BaseWeapon weap = defender.FindItemOnLayer( Layer.OneHanded ) as BaseWeapon;

			if ( weap == null )
				weap = defender.FindItemOnLayer( Layer.TwoHanded ) as BaseWeapon;

			if ( IsEvading( defender ) && BaseWeapon.CheckParry( defender ) ) {
				defender.Emote( "*evades*" ); // Yes.  Eew.  Blame OSI.
				defender.FixedEffect( 0x37B9, 10, 16 );
				return true;
			}

			return false;
		}

		public Evasion( Mobile caster, Item scroll )
			: base( caster, scroll, m_Info )
		{
		}

		public override void OnBeginCast()
		{
			base.OnBeginCast();

			Caster.FixedEffect( 0x37C4, 10, 7, 4, 3 );
		}

		public override void OnCast()
		{
			if( CheckSequence() )
			{
				Caster.SendLocalizedMessage( 1063120 ); // You feel that you might be able to deflect any attack!
				Caster.FixedParticles( 0x376A, 1, 20, 0x7F5, 0x960, 3, EffectLayer.Waist );
				Caster.PlaySound( 0x51B );

				OnCastSuccessful( Caster );

				BeginEvasion( Caster );

				Caster.BeginAction( typeof( Evasion ) );
				Timer.DelayCall( TimeSpan.FromSeconds( 20.0 ), delegate { Caster.EndAction( typeof( Evasion ) ); } );
			}

			FinishSequence();
		}

		private static Hashtable m_Table = new Hashtable();

		public static bool IsEvading( Mobile m )
		{
			return m_Table.Contains( m );
		}

		public static TimeSpan GetEvadeDuration( Mobile m )
		{
			return TimeSpan.FromSeconds( 8.0 );
		}

		public static double GetParryScalar( Mobile m )
		{
			return 1.5;
		}

		public static void BeginEvasion( Mobile m )
		{
			Timer t = (Timer)m_Table[m];

			if( t != null )
				t.Stop();

			t = new InternalTimer( m, GetEvadeDuration( m ) );

			m_Table[m] = t;

			t.Start();
		}

		public static void EndEvasion( Mobile m )
		{
			Timer t = (Timer)m_Table[m];

			if( t != null )
				t.Stop();

			m_Table.Remove( m );

			OnEffectEnd( m, typeof( Evasion ) );
		}

		private class InternalTimer : Timer
		{
			private Mobile m_Mobile;

			public InternalTimer( Mobile m, TimeSpan delay )
				: base( delay )
			{
				m_Mobile = m;
				Priority = TimerPriority.TwoFiftyMS;
			}

			protected override void OnTick()
			{
				EndEvasion( m_Mobile );
				m_Mobile.SendLocalizedMessage( 1063121 ); // You no longer feel that you could deflect any attack.
			}
		}
	}
}
