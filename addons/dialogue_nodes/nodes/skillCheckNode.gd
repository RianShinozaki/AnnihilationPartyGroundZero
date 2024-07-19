@tool
extends GraphNode


enum {EQUAL, NEQUAL, GREATER, LESS, GEQUAL, LEQUAL}

signal modified

@onready var skillType = $HBoxContainer/SkillType
@onready var difficulty = $HBoxContainer/Difficulty

@onready var trueLabel = $TrueLabel
@onready var falseLabel = $FalseLabel


func _to_dict(graph):
	var dict = {}
	dict['skillType'] = skillType.selected
	dict['difficulty'] = difficulty.selected
	
	dict['true'] = 'END'
	dict['false'] = 'END'
	
	for connection in graph.get_connection_list():
		if connection['from_node'] == name:
			if connection['from_port'] == 0:
				dict['true'] = connection['to_node']
			elif connection['from_port'] == 1:
				dict['false'] = connection['to_node']
	
	return dict


func _from_dict(_graph, dict):
	skillType.selected = dict['skillType']
	difficulty.selected = dict['difficulty']
	
	return [dict['true'], dict['false']]


func set_type(_new_type):
	_on_modified()


func set_value(_new_val):
	_on_modified()


func _on_modified():
	modified.emit()
