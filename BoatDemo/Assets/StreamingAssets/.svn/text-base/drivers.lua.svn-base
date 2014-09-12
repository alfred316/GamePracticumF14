-- These are the drivers programmed in Lua. They use the
-- LuaAPI C# script to control the boat. Some simple,
-- general drivers were programmed for basic movement.
-- However, the MoveBoat() function can be used to create
-- any elaborative movement.

startCollection = false

function MoveBoat(speed, turning, seconds)
	luabinding:addMovement(speed, turning, seconds)
	if not startCollection then ExecuteCommands() end
end

function TurnRight()
	luabinding:addMovement(0.3, 0.403, 2.0)
	if not startCollection then ExecuteCommands() end
end

function TurnLeft()
	luabinding:addMovement(0.3, -0.403, 2.0)
	if not startCollection then ExecuteCommands() end
end

function MoveForward()
	luabinding:addMovement(0.5, 0.0, 2.0)
	if not startCollection then ExecuteCommands() end
end

function StartCommandStream()
	startCollection = true
end

function ExecuteCommands()
	luabinding:delegateMoveTheBoat()
	startCollection = false
end