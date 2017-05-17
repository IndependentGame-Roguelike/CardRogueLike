namespace Assets.Script.Tools
{
    public class StaticMemberMgr
    {
        public const string GAME_SOUND_OBJ_NAME = "SoundObj";
        public const string SCENE_CONTAINERS_PATH = "SceneObj/Containers";
        public const string SCENE_TRASH_PATH = "SceneObj/Trash";
        public const string WALL_LEFT_PATH = "Wall/LeftWall";
        public const string WALL_RIGHT_PATH = "Wall/RightWall";
        public const string WALL_BOTTOM_PATH = "Wall/BottomWall";
        public const string SCENE_OBJ_NAME = "SceneObj";

        public const string SOUND_RESOURCE_PATH = "Sounds/{0}/{1}";

        public const int MAX_CARD_COUNT = 54;
        public const int MAX_ANGLE = 360;
        public const int MAX_CONTAINER = 4;
        public const int MAX_TRASH = 11;
        public const int CAN_MOVE_LAYER = 9;
        public const int FORBIDDEN_MOVE_LAYER = 10;

        public static int CurrentObjId = 0;

    }
}
