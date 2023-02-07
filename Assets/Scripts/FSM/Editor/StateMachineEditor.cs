using System;
using System.Reflection;
using UnityEditor;

namespace FSM.Tools
{
	[CustomEditor( typeof( StateMachine ) )]
	public class StateMachineEditor : Editor
	{
		private bool isTaskFoldout = true;

		public override void OnInspectorGUI()
		{
			base.OnInspectorGUI();

			StateMachine machine = target as StateMachine;

			EditorGUI.BeginDisabledGroup( true );
			EditorGUILayout.LabelField( "Info" );

			EditorGUILayout.TextField( "Current State", machine.State != null ? machine.State.name : "null" );

			if ( machine.State != null )
			{
				//  populate task
				Task task = machine.State.Task;
				if ( task != null )
				{
					Type type = task.GetType();

					isTaskFoldout = EditorGUILayout.Foldout( isTaskFoldout, "Current Task" );
					if ( isTaskFoldout )
					{
						EditorGUILayout.LabelField( type.FullName );

						//  show every fields
						EditorGUI.indentLevel++;
						FieldInfo[] fields = type.GetFields( BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public );
						foreach ( FieldInfo info in fields )
						{
							EditorGUILayout.TextField( info.Name, info.GetValue( task )?.ToString() );
						}
						EditorGUI.indentLevel--;
					}
				}
			}

			EditorGUI.EndDisabledGroup();
		}
	}
}