#@tool
extends EditorPlugin

const editorAddon = preload("res://addons/JunkBook/Scenes/control.tscn")

var dockedScene

# Called when the node enters the scene tree for the first time.
func _enter_tree():
	dockedScene = editorAddon.instance()
	add_control_to_dock(DOCK_SLOT_LEFT_UR, dockedScene)


# Called every frame. 'delta' is the elapsed time since the previous frame.
func _exit_tree():
	remove_control_from_docks(dockedScene)
	dockedScene.free()
