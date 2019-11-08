using UnityEngine;

namespace Interactables
{
    public class FireRotateScript : MonoBehaviour
    {
        public GameObject player;
        // Start is called before the first frame update
        void Start()
        {
            //testaa kun photon toimii
            player = GameObject.FindWithTag("Player");
        }

        // Update is called once per frame
        void Update()
        {
            transform.LookAt(player.transform);
        }
    }
}
