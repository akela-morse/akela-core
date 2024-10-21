﻿#if AKELA_CINEMACHINE
using Unity.Cinemachine;
using static Unity.Cinemachine.CinemachineFreeLookModifier;
using static Unity.Cinemachine.InputAxisControllerBase<Unity.Cinemachine.CinemachineInputAxisController.Reader>;

namespace Akela.Tools
{
	public static class CinemachineCameraExtensions
	{
		public readonly struct CinemachineAxisControllers
		{
			public readonly Controller orbitX;
			public readonly Controller orbitY;
			public readonly Controller orbitScale;

			public CinemachineAxisControllers(Controller orbitX, Controller orbitY, Controller orbitScale)
			{
				this.orbitX = orbitX;
				this.orbitY = orbitY;
				this.orbitScale = orbitScale;
			}
		}

		public static CinemachineAxisControllers GetControls(this CinemachineInputAxisController axisController)
		{
			Controller orbitX = null, orbitY = null, orbitScale = null;

			foreach (var controller in axisController.Controllers)
			{
				if (controller.Name.Contains(" X"))
					orbitX = controller;
				else if (controller.Name.Contains(" Y"))
					orbitY = controller;
				else if (controller.Name.Contains(" Scale"))
					orbitScale = controller;
			}

			return new CinemachineAxisControllers(orbitX, orbitY, orbitScale);
		}

		public static T GetModifier<T>(this CinemachineFreeLookModifier freeLookModifier) where T : Modifier
		{
			foreach (var modifier in freeLookModifier.Modifiers)
			{
				if (modifier is T t)
					return t;
			}

			return null;
		}
	}
}
#endif