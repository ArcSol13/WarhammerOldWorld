using System;
using System.Collections.Generic;
using System.Linq;
using TaleWorlds.Core;
using TaleWorlds.Engine;
using TaleWorlds.MountAndBlade;
using WarhammerOldWorld.CustomAgentComponents.MoraleAgentComponents;
using WarhammerOldWorld.Extensions;

namespace WarhammerOldWorld.CustomMissionLogic
{
	// Adjusts morale application logic to ignore the death of expendable units
    class CustomBaseAgentMoraleInteractionLogic : MissionLogic
    {
		public override void OnAgentRemoved(Agent affectedAgent, Agent affectorAgent, AgentState agentState, KillingBlow killingBlow)
		{
			if (affectedAgent.Character != null && ((affectorAgent != null) ? affectorAgent.Character : null) != null && (agentState == AgentState.Killed || agentState == AgentState.Unconscious) && affectedAgent.Team != null)
			{
				ValueTuple<float, float> valueTuple = MissionGameModels.Current.BattleMoraleModel.CalculateMoraleChangeAfterAgentKilled(affectedAgent, affectorAgent, WeaponComponentData.GetRelevantSkillFromWeaponClass((WeaponClass)killingBlow.WeaponClass));
				float item = valueTuple.Item1;
				float item2 = valueTuple.Item2;
				if (item == 0f && item2 == 0f)
				{
					return;
				}
				this.ApplyAoeMoraleEffect(affectedAgent, affectedAgent.GetWorldPosition(), affectorAgent.GetWorldPosition(), affectedAgent.Team, item, item2, 10f, null, null);
			}
		}

		public void ApplyAoeMoraleEffect(Agent affectedAgent, WorldPosition affectedAgentPosition, WorldPosition affectorAgentPosition, Team affectedAgentTeam, float moraleChangeAffected, float moraleChangeAffector, float radius, Predicate<Agent> affectedCondition = null, Predicate<Agent> affectorCondition = null)
		{
			if (agentCharacterIsExpendable(affectedAgent)) return;

			IEnumerable<Agent> nearbyAgents = base.Mission.GetNearbyAgents(affectedAgentPosition.AsVec2, radius);
			
			int num = 10;
			int num2 = 10;
			foreach (Agent agent in nearbyAgents)
			{
				BasicCharacterObject character = agent.Character;
				BasicCultureObject culture = character != null ? character.Culture : null;
				String cultureStringId = culture != null ? culture.StringId : null;
				if (agent.Team != null)
				{
					float num3 = agent.GetWorldPosition().GetNavMeshVec3().Distance(affectedAgentPosition.GetNavMeshVec3());
					if (num3 < radius && agent.IsAIControlled)
					{
						if (agent.Team.IsEnemyOf(affectedAgentTeam))
						{
							if (num > 0 && (affectorCondition == null || affectorCondition(agent)))
							{
								float delta = MissionGameModels.Current.BattleMoraleModel.CalculateMoraleChangeToCharacter(agent, moraleChangeAffector, num3);
								agent.getMoraleComponents().ForEach(component =>
								{
									if(component != null)
                                    {
										component.Morale += delta;
                                    }
								});
								num--;
							}
						}
						else if (num2 > 0 && (affectedCondition == null || affectedCondition(agent)))
						{
							float delta2 = MissionGameModels.Current.BattleMoraleModel.CalculateMoraleChangeToCharacter(agent, moraleChangeAffected, num3);
							agent.getMoraleComponents().ForEach(component =>
							{
								if (component != null)
								{
									component.Morale += delta2;
								}
							});
							num2--;
						}
					}
				}
			}
			//if (num2 > 0)
			//{
			//	Formation formation = affectedAgent.Formation;
			//	List<IFormationUnit> list;
			//	if (formation == null)
			//	{
			//		list = null;
			//	}
			//	else
			//	{
			//		IFormationArrangement arrangement = formation.arrangement;
			//		list = ((arrangement != null) ? arrangement.GetAllUnits() : null);
			//	}
			//	List<IFormationUnit> list2 = list;
			//	if (list2 != null)
			//	{
			//		HashSet<int> hashSet = new HashSet<int>();
			//		int count = list2.Count;
			//		int num4 = Math.Min(num2, list2.Count);
			//		for (int i = count - num4; i < count; i++)
			//		{
			//			int num5 = MBRandom.RandomInt(0, i + 1);
			//			hashSet.Add(hashSet.Contains(num5) ? i : num5);
			//		}
			//		foreach (int index in hashSet)
			//		{
			//			Agent agent2 = list2[index] as Agent;
			//			if (agent2 != null && agent2.IsActive() && agent2.IsAIControlled)
			//			{
			//				float distance = agent2.GetWorldPosition().GetNavMeshVec3().Distance(affectedAgentPosition.GetNavMeshVec3());
			//				float delta3 = MissionGameModels.Current.BattleMoraleModel.CalculateMoraleChangeToCharacter(agent2, moraleChangeAffected, distance);
			//				agent2.ChangeMorale(delta3);
			//			}
			//		}
			//	}
			//}
		}

		public bool agentCharacterIsExpendable(Agent agent)
        {
			IEnumerable<Agent> nearbyAgents = base.Mission.GetNearbyAgents(agent.GetWorldPosition().AsVec2, 10f);
			BasicCharacterObject affectedCharacter = agent.Character;

			if (affectedCharacter != null)
			{
				if (affectedCharacter.getAttributes().Contains("Expendable"))
				{
					Helpers.Say(affectedCharacter.GetName() + " died, but is Expendable. Nearby allies lost 0 morale.");
					float moraleSum = 0f;
					nearbyAgents.ToList().ForEach(a =>
					{
						VampireMoraleAgentComponent moraleComponent = a.GetComponent<VampireMoraleAgentComponent>();
						if (moraleComponent != null)
						{
							moraleSum += moraleComponent.Morale;
						}
						else if (a.GetComponent<MoraleAgentComponent>() != null)
						{
							moraleSum += a.GetMorale();
						}
					}
					);
					float averageMorale = moraleSum / nearbyAgents.ToList().Count;
					Helpers.Say("Average morale of nearby allies: " + averageMorale);
					return true;
				}
			}
			return false;
		}
	}
}
