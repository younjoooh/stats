using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class MedianGate : MonoBehaviour
{
    public int[] RandomNumbers = new int[8];
    public int[] OrderedNumbers;
    public GameObject[] Points = new GameObject[8];

    public GameObject[] Numbers = new GameObject[8];
    public GameObject[] Locations = new GameObject[8];

    public GameObject SmallBorder;
    public GameObject BigBorder;
    public GameObject AddedTech;
    public GameObject DividedTech;
    public GameObject Spinner;
    public GameObject Min;
    public GameObject Max;
    public GameObject Chamber;
    public GameObject FireEffect;
    public GameObject LostSpirit;
    public GameObject TextBR;


    public bool OrderNumnders = false;
    public bool GrowAddedTech = false;
    public bool ShowAddedTech = false;
    public bool GrowDividedTech = false;
    public bool GateFinished = false;
    public bool MoveUp = false;

    public int PointsSelected = 0;
    public GameObject GrowPoint;

    float OrderNumbersSpeed = 2;
    float SmallBorderSpeed = 8;
    public string SmallBorderLocation = "0";

    GameObject SelectEffect;
    GameObject ExplodeEffect;
    AudioSource ExplosionSound;

    public GameObject MedianText;
    public bool PlayerFound;
    GameObject Player;

    public string WaveType = "Ice";

    public AudioSource MedianAudio, MergeAudio;
    void Start()
    {
        //WaveType = "Ice";

        // ORDER NUMBERS
        if (tag != "Upper" && tag != "Lower")
        {
            for (int i = 0; i <= 7; i++)
            {
                RandomNumbers[i] = UnityEngine.Random.Range(1, 100);
            }
        }

        if (WaveType == "Ice")
        {
            if (tag == "Lower")
            {
                RandomNumbers[0] = 10;
                RandomNumbers[1] = 11;
                RandomNumbers[2] = 15;
                RandomNumbers[3] = 20;
                RandomNumbers[4] = 24;
                RandomNumbers[5] = 26;
                RandomNumbers[6] = 30;
                RandomNumbers[7] = 30;
            }

            if (tag == "Upper")
            {
                RandomNumbers[0] = 32;
                RandomNumbers[1] = 35;
                RandomNumbers[2] = 35;
                RandomNumbers[3] = 40;
                RandomNumbers[4] = 40;
                RandomNumbers[5] = 45;
                RandomNumbers[6] = 50;
                RandomNumbers[7] = 50;
            }
        }

        if (WaveType == "Fire")
        {
            if (tag == "Lower")
            {
                RandomNumbers[0] = 12;
                RandomNumbers[1] = 12;
                RandomNumbers[2] = 16;
                RandomNumbers[3] = 22;
                RandomNumbers[4] = 26;
                RandomNumbers[5] = 27;
                RandomNumbers[6] = 32;
                RandomNumbers[7] = 32;
            }

            if (tag == "Upper")
            {
                RandomNumbers[0] = 36;
                RandomNumbers[1] = 38;
                RandomNumbers[2] = 38;
                RandomNumbers[3] = 42;
                RandomNumbers[4] = 44;
                RandomNumbers[5] = 48;
                RandomNumbers[6] = 52;
                RandomNumbers[7] = 52;
            }
        }




        if (WaveType == "Poison")
        {
            if (tag == "Lower")
            {
                RandomNumbers[0] = 11;
                RandomNumbers[1] = 12;
                RandomNumbers[2] = 15;
                RandomNumbers[3] = 19;
                RandomNumbers[4] = 19;
                RandomNumbers[5] = 21;
                RandomNumbers[6] = 28;
                RandomNumbers[7] = 29;
            }

            if (tag == "Upper")
            {
                RandomNumbers[0] = 32;
                RandomNumbers[1] = 33;
                RandomNumbers[2] = 39;
                RandomNumbers[3] = 39;
                RandomNumbers[4] = 41;
                RandomNumbers[5] = 44;
                RandomNumbers[6] = 47;
                RandomNumbers[7] = 93;
            }
        }

        if (WaveType == "Electric")
        {
            if (tag == "Lower")
            {
                RandomNumbers[0] = 13;
                RandomNumbers[1] = 14;
                RandomNumbers[2] = 18;
                RandomNumbers[3] = 17;
                RandomNumbers[4] = 19;
                RandomNumbers[5] = 27;
                RandomNumbers[6] = 29;
                RandomNumbers[7] = 32;
            }

            if (tag == "Upper")
            {
                RandomNumbers[0] = 36;
                RandomNumbers[1] = 40;
                RandomNumbers[2] = 40;
                RandomNumbers[3] = 43;
                RandomNumbers[4] = 44;
                RandomNumbers[5] = 49;
                RandomNumbers[6] = 51;
                RandomNumbers[7] = 89;
            }
        }

        OrderedNumbers = (int[])RandomNumbers.Clone();
        System.Array.Sort(OrderedNumbers);


        // MEDIAN GATE OBJECT
        for (int i = 0; i <= 7; i++)
        {
            Numbers[i] = transform.GetChild(i).gameObject;
        }

        for (int i = 0; i <= 7; i++)
        {
            Locations[i] = transform.GetChild(i + 8).gameObject;
        }

        for (int i = 0; i <= 7; i++)
        {
            Locations[i].gameObject.SetActive(false);
        }

        SmallBorder = transform.GetChild(16).gameObject;
        BigBorder = transform.GetChild(17).gameObject;
        AddedTech = transform.GetChild(18).gameObject;
        DividedTech = transform.GetChild(19).gameObject;
        Spinner = transform.GetChild(20).gameObject;

        SmallBorder.SetActive(false);

        BigBorder.SetActive(false);
        BigBorder.transform.localScale = new Vector3(0, 0, 0);

        AddedTech.SetActive(false);
        AddedTech.transform.localScale = new Vector3(1.605688f, 1.204267f, 0.124441f) * .25f;

        DividedTech.SetActive(false);
        DividedTech.transform.localScale = new Vector3(0, 0, 0);

        for (int i = 0; i <= 7; i++)
        {
            Numbers[i].transform.GetChild(0).transform.GetChild(0).transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = OrderedNumbers[i].ToString();
        }

        ExplodeEffect = GameObject.Instantiate(Resources.Load<GameObject>("Effects/Median Explosion"), new Vector3(0,0,0), Quaternion.identity);
        ExplodeEffect.SetActive(false);

        ExplosionSound = gameObject.AddComponent<UnityEngine.AudioSource>();
        ExplosionSound.clip = Resources.Load<AudioClip>("Audio/Mech Sounds/Mech Explosion Sound");
        ExplosionSound.spatialBlend = 1;
        ExplosionSound.volume = 1f;
        ExplosionSound.playOnAwake = false;

        MedianText.GetComponent<Text>().text = " ";
        Player = GameObject.FindWithTag("Player");

        if (Min != null)
        {
            Min.SetActive(false);
        }

        if (Max != null)
        {
            Max.SetActive(false);
        }

        MedianAudio = gameObject.AddComponent<UnityEngine.AudioSource>();
        MedianAudio.clip = Resources.Load<AudioClip>("Audio/Median Audio/1");
        MedianAudio.spatialBlend = 0;
        MedianAudio.volume = 1f;
        MedianAudio.playOnAwake = false;

        MergeAudio = gameObject.AddComponent<UnityEngine.AudioSource>();
        MergeAudio.clip = Resources.Load<AudioClip>("Audio/Median Audio/Merge Audio");
        MergeAudio.spatialBlend = 0;
        MergeAudio.volume = .1f;
        MergeAudio.playOnAwake = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (PlayerFound)
        {
            MedianText.SetActive(true);
        }

        if (!PlayerFound)
        {
            MedianText.SetActive(false);
        }

        if (PlayerFound && !DividedTech.activeSelf) {
            if (PointsSelected == 0 && !OrderNumnders && MedianText.GetComponent<Text>().text == " " && tag != "Upper" && tag != "Lower")
            {
                MedianText.GetComponent<Text>().text = "This is called a Median Gate, you have to blow it up to get to the other side." +
                                                         "\nThe only way to unlock it, is to solve a median algarithm using your bullets." +
                                                       "\n\nStart by shooting each one of those points on the circle to generate random numbers.";
            }


            if (PointsSelected == 0 && !OrderNumnders && MedianText.GetComponent<Text>().text == " " && tag == "Upper")
            {
                MedianText.GetComponent<Text>().text = "Now that you've added the Lower Quartile to the plot and you've added the minimum value, " +
                                                     "\nLet's plot the Upper Quartile and the maximum value. Start by shooting each one of those " +
                                                     "\nblue circles to reveal the data you collected from the Lost Spirits weights";
                if (!MedianAudio.isPlaying)
                {
                    MedianAudio.clip = Resources.Load<AudioClip>("Audio/Median Audio/1A");
                    MedianAudio.Stop();
                    MedianAudio.PlayOneShot(MedianAudio.clip, 1f);
                }
            }



            if (PointsSelected == 0 && !OrderNumnders && MedianText.GetComponent<Text>().text == " " && tag == "Lower")
            {
                MedianText.GetComponent<Text>().text = "This is Called a Box and Whisker Plot. You need to complete it using the data you collected from the lost Spirit's weights." +
                                                     "\nOnce you've completed it, you will be able to summon a Lost Spirit that will become your ally and assist you in battle." +
                                                   "\n\nIn order to complete the plot, you need to find the values for the Upper Quartiles, Lower Quartiles, and the Max and Min" +
                                                     "\nStart by shooting each one of those blue circles to reveal the data you collected from the Lost Spirits weights";

                if (!MedianAudio.isPlaying)
                {
                    MedianAudio.clip = Resources.Load<AudioClip>("Audio/Median Audio/1B");
                    MedianAudio.Stop();
                    MedianAudio.PlayOneShot(MedianAudio.clip, 1f);
                }
            }
        }


        if (PointsSelected == 8 && !OrderNumnders)
        {
            if (tag != "Upper" && tag != "Lower")
                MedianText.GetComponent<Text>().text = "Now That you have generated random numbers, \npress the [ T ] Key to put the numbers in order from Least to greatest";

            if (tag == "Upper")
            {
                MedianText.GetComponent<Text>().text = "Now That you have revealed your data from the Upper Quartile, \npress the [ T ] Key to put the numbers in order from Least to greatest";
                if (MedianAudio.clip != Resources.Load<AudioClip>("Audio/Median Audio/2"))
                {
                    MedianAudio.clip = Resources.Load<AudioClip>("Audio/Median Audio/2");
                    MedianAudio.Stop();
                    MedianAudio.PlayOneShot(MedianAudio.clip, 1f);
                }
            }

            if (tag == "Lower")
            {
                MedianText.GetComponent<Text>().text = "Now That you have revealed your data from the Lower Quartile, \npress the [ T ] Key to put the numbers in order from Least to greatest";
                if (MedianAudio.clip != Resources.Load<AudioClip>("Audio/Median Audio/3"))
                {
                    MedianAudio.clip = Resources.Load<AudioClip>("Audio/Median Audio/3");
                    MedianAudio.Stop();
                    MedianAudio.PlayOneShot(MedianAudio.clip, 1f);
                }
            }
        }
        if (Input.GetKeyDown("t") && PointsSelected == 8)
        {
            OrderNumnders = true;
            PointsSelected = 0;

            MergeAudio.PlayOneShot(MergeAudio.clip, 1f);
            if (tag != "Upper" && tag != "Lower")
                MedianText.GetComponent<Text>().text = "Now That the numbers are all in order, keep shooting at the blue box to knock out numbers until your left with the middle values!";

            if (tag == "Upper")
            {
                MedianText.GetComponent<Text>().text = "Now That the numbers in the Lower Quartile are all in order, You can see that" +
                                                     "\nthe minimum value of the data has been added to the left hand side of the plot" +
                                                     "\nKeep shooting at the blue box to knock out the numbers until your left with the middle values!";
                if (MedianAudio.clip != Resources.Load<AudioClip>("Audio/Median Audio/4"))
                {
                    MedianAudio.clip = Resources.Load<AudioClip>("Audio/Median Audio/4");
                    MedianAudio.Stop();
                    MedianAudio.PlayOneShot(MedianAudio.clip, 1f);
                }
            }

            if (tag == "Lower")
            {
                MedianText.GetComponent<Text>().text = "Now That the numbers in the Upper Quartile are all in order, You can see that" +
                                                     "\nthe maximum value of the data has been added to the Right hand side of the plot" + 
                                                     "\nKeep shooting at the blue box to knock out the numbers until your left with the middle values!";
                if (MedianAudio.clip != Resources.Load<AudioClip>("Audio/Median Audio/5"))
                {
                    MedianAudio.clip = Resources.Load<AudioClip>("Audio/Median Audio/5");
                    MedianAudio.Stop();
                    MedianAudio.PlayOneShot(MedianAudio.clip, 1f);
                }
            }

            Player.GetComponent<Animator>().SetBool("Order Numbers", true);
            if (tag == "Upper")
            {
                Max.SetActive(true);
            }
            if (tag == "Lower")
            {
                Min.SetActive(true);
            }

        }



        if (Player.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("Order Numbers"))
        {
            Player.GetComponent<Animator>().SetBool("Order Numbers", false);
        }

        if (GrowPoint != null)
        {
            if (GrowPoint.transform.localScale != new Vector3(0.8379467f, 1.117262f, 10.8122f))
            {
                GrowPoint.transform.localScale = Vector3.MoveTowards(GrowPoint.transform.localScale, new Vector3(0.8379467f, 1.117262f, 10.8122f), 25 * Time.deltaTime);
            }
        }

        // PUT NUMBERS IN ORDER
        if (OrderNumnders && Numbers[7].transform.position != Locations[7].transform.position)
        {
            SmallBorder.SetActive(true);
            for (int i = 0; i <= 7; i++)
            {
                Numbers[i].transform.position = Vector3.Lerp(Numbers[i].transform.position, Locations[i].transform.position, OrderNumbersSpeed * Time.deltaTime);
            }
        }
        else
        {
            OrderNumnders = false;
        }

        // MOVE SMALL BORDER TO CORRECT LOCATION
        SmallBorder.transform.position = Vector3.MoveTowards(SmallBorder.transform.position, Locations[int.Parse(SmallBorderLocation)].transform.position, SmallBorderSpeed * Time.deltaTime);
        SmallBorder.transform.name = SmallBorderLocation;


        // GROW BIG BORDER/ SHRINK SMALL BORDER
        if (SmallBorderLocation == "3")
        {
            BigBorder.SetActive(true);
            BigBorder.transform.localScale = Vector3.MoveTowards(BigBorder.transform.localScale, new Vector3(1.35f, 0.475f, 0.05508f), 2 * Time.deltaTime);
            SmallBorder.SetActive(false);
            MedianText.GetComponent<Text>().text = "you ended up with 2 numbers in the middle, to find the Median, we'll have to average these 2 numbers." +
                                                 "\nShoot at the purple box to add these 2 numbers together";
            if (MedianAudio.clip != Resources.Load<AudioClip>("Audio/Median Audio/6"))
            {
                MedianAudio.clip = Resources.Load<AudioClip>("Audio/Median Audio/6");
                MedianAudio.Stop();
                MedianAudio.PlayOneShot(MedianAudio.clip, 1f);
            }

        }

        // GROW ADDED TECH / SHRINK BIG BORDER
        if (GrowAddedTech)
        {
            SmallBorderLocation = "5";
            BigBorder.transform.localScale = Vector3.MoveTowards(BigBorder.transform.localScale, new Vector3(0.7106537f, BigBorder.transform.localScale.y, BigBorder.transform.localScale.z), 2f * Time.deltaTime);
            Numbers[3].transform.position = Vector3.MoveTowards(Numbers[3].transform.position, DividedTech.transform.position + DividedTech.transform.forward * .25f, 2 * Time.deltaTime);
            Numbers[4].transform.position = Vector3.MoveTowards(Numbers[4].transform.position, DividedTech.transform.position + DividedTech.transform.forward * .25f, 2 * Time.deltaTime);
            MedianText.GetComponent<Text>().text = "Now that you've aded the numbers together, move up close to the gate" +
                                                "\n and press the [ y ] key to use your divide sword and dived the number in half!";
            if (MedianAudio.clip != Resources.Load<AudioClip>("Audio/Median Audio/7"))
            {
                MedianAudio.clip = Resources.Load<AudioClip>("Audio/Median Audio/7");
                MedianAudio.Stop();
                MedianAudio.PlayOneShot(MedianAudio.clip, 1f);
            }
        }

        if (Numbers[4].transform.position == DividedTech.transform.position + DividedTech.transform.forward * .25f)
        {
            GrowAddedTech = false;
            BigBorder.SetActive(false);
            Numbers[3].SetActive(false);
            Numbers[4].SetActive(false);
            ShowAddedTech = true;
        }


        // SHOW ADDED TECH
        if (ShowAddedTech)
        {
            AddedTech.SetActive(true);
            AddedTech.transform.GetChild(0).transform.GetChild(0).transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = (OrderedNumbers[3] + OrderedNumbers[4]).ToString();
            AddedTech.transform.localScale = Vector3.MoveTowards(AddedTech.transform.localScale, new Vector3(1.605688f, 1.204267f, 0.124441f), 2f * Time.deltaTime);

            Numbers[4].transform.position = new Vector3(DividedTech.transform.position.x + 1f, Numbers[4].transform.position.y, Numbers[4].transform.position.z);
        }

        if (AddedTech.GetComponent<RotateAroundAxis>() != null)
        {

            if (AddedTech.GetComponent<RotateAroundAxis>().Speed > -2000)
            {
                AddedTech.GetComponent<RotateAroundAxis>().Speed -= 500 * Time.deltaTime;
            }

            if (!GateFinished && AddedTech.GetComponent<RotateAroundAxis>().Speed <= -2000)
            {
                if (tag != "Upper" && tag != "Lower")
                    MedianText.GetComponent<Text>().text = "you did it! the gate will explode in 3... 2... 1...";

                if (tag == "Upper")
                {
                    MedianText.GetComponent<Text>().text = "you did it! The vaule for the Upper Quartile will be added to the plot in 3... 2... 1...";
                    if (MedianAudio.clip != Resources.Load<AudioClip>("Audio/Median Audio/8"))
                    {
                        MedianAudio.clip = Resources.Load<AudioClip>("Audio/Median Audio/8");
                        MedianAudio.Stop();
                        MedianAudio.PlayOneShot(MedianAudio.clip, 1f);
                    }
                }

                if (tag == "Lower")
                {
                    MedianText.GetComponent<Text>().text = "you did it! the vaule for the Lower Quartile will be added to the plot in 3... 2... 1...";
                    if (MedianAudio.clip != Resources.Load<AudioClip>("Audio/Median Audio/9"))
                    {
                        MedianAudio.clip = Resources.Load<AudioClip>("Audio/Median Audio/9");
                        MedianAudio.Stop();
                        MedianAudio.PlayOneShot(MedianAudio.clip, 1f);
                    }
                }

                AddedTech.transform.localScale = Vector3.MoveTowards(AddedTech.transform.localScale, new Vector3(0, 0, 0), 5 * Time.deltaTime);
                DividedTech.SetActive(true);
                DividedTech.transform.GetChild(0).transform.GetChild(0).transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = ((OrderedNumbers[3] + OrderedNumbers[4]) / 2).ToString();
                DividedTech.transform.localScale = Vector3.MoveTowards(DividedTech.transform.localScale, new Vector3(1.605688f, 1.204267f, 0.124441f), 5 * Time.deltaTime);
                Spinner.transform.localScale = Vector3.MoveTowards(Spinner.transform.localScale, new Vector3(0, 0, 0), 10 * Time.deltaTime);
            }
        }



        // MOVE UP
        if (MoveUp)
        {
            DividedTech.transform.localScale = new Vector3(0.675f, .5f, 0.001f);
        }


        if (!PlayerFound)
        {
            MedianText.GetComponent<Text>().text = " ";
        }

    }

    IEnumerator Explode()
    {
        yield return new WaitForSeconds(10);
        GateFinished = true;

        AudioSource.PlayClipAtPoint(ExplosionSound.clip, transform.position);

        if (tag == "Lower")
        {
                MedianText.GetComponent<Text>().text = "Now Head to the right side of the Box and Whisker plot and let's plot some data points for the Upper Quartile";

                if (MedianAudio.clip != Resources.Load<AudioClip>("Audio/Median Audio/15"))
                {
                    MedianAudio.clip = Resources.Load<AudioClip>("Audio/Median Audio/15");
                    MedianAudio.Stop();
                    MedianAudio.PlayOneShot(MedianAudio.clip, 1f);
                }
        }
        else 
        {
            MedianText.GetComponent<Text>().text = " ";
        }


        if (tag != "Upper" && tag != "Lower")
        {
            ExplodeEffect.SetActive(false);
            ExplodeEffect.SetActive(true);
            ExplodeEffect.transform.position = DividedTech.transform.position + DividedTech.transform.forward * 2.5f;
            Destroy(this.gameObject);
        }
        else
        {
            for (int i = 0; i <= 7; i++)
            {
                Destroy(Points[i]);
            }
            DividedTech.transform.localScale = new Vector3(0, 0, 0);
            DividedTech.transform.localPosition = new Vector3(DividedTech.transform.localPosition.x, DividedTech.transform.localPosition.y + 2, DividedTech.transform.localPosition.z) - DividedTech.transform.forward * 1.5f;
            MoveUp = true;
        }

        //StartCoroutine(RemoveText());
    }

    IEnumerator RemoveText()
    {
        yield return new WaitForSeconds(4);
        MedianText.GetComponent<Text>().text = " ";
    }


        private void OnTriggerStay(Collider Coll)
    {
        if (Coll.gameObject.transform.tag == "Player")
        {
            PlayerFound = true;
            MedianText.SetActive(true);
        }

    }

    private void OnTriggerExit(Collider Coll)
    {
        if (Coll.gameObject.transform.tag == "Player")
        {
            PlayerFound = false;
            MedianText.SetActive(false);

        }

    }
}
