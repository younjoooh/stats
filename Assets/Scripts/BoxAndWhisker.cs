using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class BoxAndWhisker : MonoBehaviour
{
    public GameObject Upper;
    public GameObject Lower;
    public GameObject LostSpirit;
    public GameObject SummonEffect;
    public GameObject PlotText;
    public GameObject Chamber;

    public bool MoveUp;
    public AudioSource MedianAudio;
    public Vector3 StartPositon;

    public GameObject ParasiteGroup;
    public int Level;

    public int UpperPhase = 0;
    public int LowerPhase = 0;

    public bool Started;
    public bool OutlierFound;

    public GameObject UpperMarker;
    public GameObject LowerMarker;

    public GameObject IQR;
    public GameObject IQRMultiplied;

    public GameObject RangeBrackets;
    public GameObject Outlier;

    void Start()
    {
        MedianAudio = gameObject.AddComponent<UnityEngine.AudioSource>();
        MedianAudio.clip = Resources.Load<AudioClip>("Audio/Median Audio/1");
        MedianAudio.spatialBlend = 0;
        MedianAudio.volume = 1f;
        MedianAudio.playOnAwake = false;

        StartPositon = transform.position;
        ParasiteGroup.transform.parent = null;
        Started = false;

        IQR.transform.localScale = new Vector3(0, 0, 0);
        IQRMultiplied.transform.localScale = new Vector3(0, 0, 0);

        UpperMarker.SetActive(false);
        LowerMarker.SetActive(false);
        RangeBrackets.SetActive(false);

        UpperPhase = 0;
        LowerPhase = 0;

        RangeBrackets.transform.GetChild(7).transform.GetChild(0).transform.localScale = new Vector3(75, 75, 75);
        RangeBrackets.transform.GetChild(7).transform.GetChild(2).transform.localScale = new Vector3(75, 75, 75);

        Outlier.transform.localScale = new Vector3(0,0,0);

    }
    void Update()
    {
        if (Started)
        {
            if (RangeBrackets.transform.GetChild(7).transform.GetChild(0).GetComponent<TextMeshProUGUI>().text != "[   ]" && RangeBrackets.transform.GetChild(7).transform.GetChild(0).transform.localScale != new Vector3(1, 1, 1))
            {
                RangeBrackets.transform.GetChild(7).transform.GetChild(0).transform.localScale = Vector3.MoveTowards(RangeBrackets.transform.GetChild(7).transform.GetChild(0).transform.localScale, new Vector3(1, 1, 1), 400 * Time.deltaTime);
            }

            if (RangeBrackets.transform.GetChild(7).transform.GetChild(2).GetComponent<TextMeshProUGUI>().text != "[   ]" && RangeBrackets.transform.GetChild(7).transform.GetChild(2).transform.localScale != new Vector3(1, 1, 1))
            {
                RangeBrackets.transform.GetChild(7).transform.GetChild(2).transform.localScale = Vector3.MoveTowards(RangeBrackets.transform.GetChild(7).transform.GetChild(2).transform.localScale, new Vector3(1, 1, 1), 400 * Time.deltaTime);
            }

            if (Outlier.activeSelf)
            {
                Outlier.transform.localScale = Vector3.MoveTowards(Outlier.transform.localScale, new Vector3(0.4854499f, 0.35959f, 0.00072f), 3 * Time.deltaTime);
            }
        }

        if (Upper.activeSelf && Lower.activeSelf && MedianAudio.clip == Resources.Load<AudioClip>("Audio/Median Audio/1") )
        {
            print("Next 1");
            if (Upper.transform.localScale == new Vector3(0.675f, .5f, 0.001f) && Lower.transform.localScale == new Vector3(0.675f, .5f, 0.001f))
            {
                print("Next 2");
                PlotText.SetActive(true);

                if (Level == 5 || (gameObject.name == "Box & Whisker - Poison" && OutlierFound) || (gameObject.name == "Box & Whisker - Electric" && OutlierFound))
                {
                    print("Next 3");
                    PlotText.GetComponent<Text>().text = "now that you've completd the box and whisker Plot, you can summon a Lost Spirit as you ally by pressing the [ g ] key";

                    if (MedianAudio.clip != Resources.Load<AudioClip>("Audio/Median Audio/10"))
                    {
                        MedianAudio.clip = Resources.Load<AudioClip>("Audio/Median Audio/10");
                        MedianAudio.Stop();
                        MedianAudio.PlayOneShot(MedianAudio.clip, 1f);
                    }
                }

                if (Level == 6 && !OutlierFound)
                {
                    print("Next 4");
                    Started = true;
                    PlotText.GetComponent<Text>().text = "Now that you've found the Lower Quartile, Upper Quartile, and Max and Min," +
                                                       "\nwe need to find out if there are any outliers, then graph them on the Box and whisker plot." +
                                                       "\nbut before we can find the outliers, we'll need to find the Inter Quartile Range or [IQR] for short." +
                                                       "\nTo find the IQR, we have to subtract the Lower Quartile from the Upper Quartile." +
                                                       "\nTo start subtracting, first shoot a blaster at the Upper Quartile to mark it.";

                    if (MedianAudio.clip != Resources.Load<AudioClip>("Audio/Median Audio/19"))
                    {
                        MedianAudio.clip = Resources.Load<AudioClip>("Audio/Median Audio/19");
                        MedianAudio.Stop();
                        MedianAudio.PlayOneShot(MedianAudio.clip, 1f);
                    }

                    StartCoroutine(ActivateUpperMarker(18));
                }

            }
        }

        if (Upper.activeSelf && Lower.activeSelf)
        {
            if (Upper.transform.localScale == new Vector3(0.675f, .5f, 0.001f) && Lower.transform.localScale == new Vector3(0.675f, .5f, 0.001f))
            {

                if (Input.GetKeyDown("g"))
                {
                    StartCoroutine("PlaySummonEffect");
                    StartCoroutine("SummonLostSpirit");
                }
            }
        }

        // SPIN CHAMBER
        if (Chamber.GetComponent<RotateAroundAxis>() != null)
        {
            Chamber.GetComponent<RotateAroundAxis>().Target = Chamber;
            Chamber.GetComponent<RotateAroundAxis>().Angle = new Vector3(0, 1, 0);
            if (Chamber.GetComponent<RotateAroundAxis>().Speed > -2000)
            {
                Chamber.GetComponent<RotateAroundAxis>().Speed -= 500 * Time.deltaTime;
            }

        }

        // MOVE UP
        if (MoveUp)
        {
            transform.position = Vector3.MoveTowards(transform.position, StartPositon + new Vector3(0, 100, 0), 3 * Time.deltaTime);
        }


        if (Input.GetKeyDown("k"))
        {
            StartCoroutine("PlaySummonEffect");
            StartCoroutine("SummonLostSpirit");
        }

        // GROW IQR
        if (IQR.activeSelf)
        {
            IQR.transform.localScale = Vector3.MoveTowards(IQR.transform.localScale, new Vector3(1, 1, 1), 5 * Time.deltaTime);
        }

        // GROW IQR MULTIPLIED
        if (IQRMultiplied.activeSelf)
        {
            IQRMultiplied.transform.localScale = Vector3.MoveTowards(IQRMultiplied.transform.localScale, new Vector3(1, 1, 1), 5 * Time.deltaTime);
        }
    }

    public void UpperHasBeenShot()
    {
        UpperPhase = 1;
        PlotText.GetComponent<Text>().text = "Now that you've Marked the Upper Quartile, shoot at the lower Quartile to subtract it!";

        if (MedianAudio.clip != Resources.Load<AudioClip>("Audio/Median Audio/20"))
        {
            MedianAudio.clip = Resources.Load<AudioClip>("Audio/Median Audio/20");
            MedianAudio.Stop();
            MedianAudio.PlayOneShot(MedianAudio.clip, 1f);

            MedianAudio.clip = Resources.Load<AudioClip>("Audio/Median Audio/Merge Audio");
            MedianAudio.PlayOneShot(MedianAudio.clip, 1f);
        }

        StartCoroutine(ActivateLowerMarker(4.5f));
    }

    public void LowerHasBeenShot()
    {
        LowerPhase = 1;
        LowerMarker.SetActive(false);
        IQR.SetActive(true);

        PlotText.GetComponent<Text>().text = "Good Job, we found the IQR! The next step in finding any outliers is to multiply the IQR by 1.5." +
                                           "\nShoot a blaster at the IQR to multiply it by 1.5!";

        if (MedianAudio.clip != Resources.Load<AudioClip>("Audio/Median Audio/21"))
        {
            MedianAudio.clip = Resources.Load<AudioClip>("Audio/Median Audio/21");
            MedianAudio.Stop();
            MedianAudio.PlayOneShot(MedianAudio.clip, 1f);

            MedianAudio.clip = Resources.Load<AudioClip>("Audio/Median Audio/Merge Audio");
            MedianAudio.PlayOneShot(MedianAudio.clip, 1f);
        }
    }

    public void IQRHasBeenShot()
    {
        IQR.SetActive(false);
        IQRMultiplied.SetActive(true);
        RangeBrackets.SetActive(true);
        PlotText.GetComponent<Text>().text = "Now that we have the IQR multiplied by 1.5, we can find a range that will help us discover any outliers." +
                                             "\nAn outlier is any number that lies out side of this set range." +
                                             "\n\nTo find the smallest number in this range, we have to subtract our new IQR value from the Lower Quartile." +
                                             "\nTo find the Largest number in this range we have to Add our new IQR value to the Upper Quartile." +
                                             "\nNow lets's start by finding the smallest number in the range, first shoot a blaster at the Lower Quartile!";



        if (MedianAudio.clip != Resources.Load<AudioClip>("Audio/Median Audio/22"))
        {
            MedianAudio.clip = Resources.Load<AudioClip>("Audio/Median Audio/22");
            MedianAudio.Stop();
            MedianAudio.PlayOneShot(MedianAudio.clip, 1f);

            MedianAudio.clip = Resources.Load<AudioClip>("Audio/Median Audio/Merge Audio");
            MedianAudio.PlayOneShot(MedianAudio.clip, 1f);
        }


        StartCoroutine(ActivateLowerMarker(23f));
    }

    public void LowerHasBeenShot2()
    {
        LowerPhase = 2;

        if (gameObject.name == "Box & Whisker - Poison")
        {
            RangeBrackets.transform.GetChild(7).transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "-13";
        }

        if (gameObject.name == "Box & Whisker - Electric")
        {
            RangeBrackets.transform.GetChild(7).transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "-13";
        }

        LowerMarker.SetActive(false);
        UpperMarker.SetActive(true);
        PlotText.GetComponent<Text>().text = "You've subtracted the new IQR value from the Lower Quartile and got the value shown below in the brackets!" +
                                           "\nThe value on the left side of the brackets, is the smallest number in the range. Now let's add the new IQR" +
                                           "\nvalue to the Uppe Quartile to find the largest number in this range." +
                                           "\nTo do this, just Shoot a blaster at the Upper Quartile!";



        if (MedianAudio.clip != Resources.Load<AudioClip>("Audio/Median Audio/23"))
        {
            MedianAudio.clip = Resources.Load<AudioClip>("Audio/Median Audio/23");
            MedianAudio.Stop();
            MedianAudio.PlayOneShot(MedianAudio.clip, 1f);

            MedianAudio.clip = Resources.Load<AudioClip>("Audio/Median Audio/Merge Audio");
            MedianAudio.PlayOneShot(MedianAudio.clip, 1f);
        }
    }

    public void UpperHasBeenShot2()
    {
        UpperPhase = 2;
        if (gameObject.name == "Box & Whisker - Poison")
        {
            RangeBrackets.transform.GetChild(7).transform.GetChild(2).GetComponent<TextMeshProUGUI>().text = "72";
        }
        if (gameObject.name == "Box & Whisker - Electric")
        {
            RangeBrackets.transform.GetChild(7).transform.GetChild(2).GetComponent<TextMeshProUGUI>().text = "81";
        }

        UpperMarker.SetActive(false);
        PlotText.GetComponent<Text>().text = "You've Added the new IQR value to the Upper Quartile and got the value shown on the right side of the brackets!" +
                                             "\nthis is the Largest number in the range! Now to find an outlier, we'll have to check our data" +
                                             "\nto see if any of the lost spirits weights are either above or bellow this range.";



        if (MedianAudio.clip != Resources.Load<AudioClip>("Audio/Median Audio/24"))
        {
            MedianAudio.clip = Resources.Load<AudioClip>("Audio/Median Audio/24");
            MedianAudio.Stop();
            MedianAudio.PlayOneShot(MedianAudio.clip, 1f);

            MedianAudio.clip = Resources.Load<AudioClip>("Audio/Median Audio/Merge Audio");
            MedianAudio.PlayOneShot(MedianAudio.clip, 1f);
        }

        StartCoroutine(DelayFindOutlier(15));
    }


    IEnumerator PlaySummonEffect()
    {
        yield return new WaitForSeconds(0);
        SummonEffect.SetActive(true);
        SummonEffect.transform.parent = null;

        PlotText.GetComponent<Text>().text = "You are Now Summoning a Lost Spirit!";
        if (MedianAudio.clip != Resources.Load<AudioClip>("Audio/Median Audio/11"))
        {
            MedianAudio.clip = Resources.Load<AudioClip>("Audio/Median Audio/11");
            MedianAudio.Stop();
            MedianAudio.PlayOneShot(MedianAudio.clip, 1f);
        }

        Chamber.AddComponent<RotateAroundAxis>();

    }
    IEnumerator SummonLostSpirit()
    {
        yield return new WaitForSeconds(6f);
        LostSpirit.SetActive(true);
        LostSpirit.transform.parent = null;
        PlotText.GetComponent<Text>().text = "You now have a lost spirit as your ally, this lost spirit will follow you aroud and use it's elemental abilities to assist you in battle!";
        if (MedianAudio.clip != Resources.Load<AudioClip>("Audio/Median Audio/12"))
        {
            MedianAudio.clip = Resources.Load<AudioClip>("Audio/Median Audio/12");
            MedianAudio.Stop();
            MedianAudio.PlayOneShot(MedianAudio.clip, 1f);
        }
        MoveUp = true;
        StartCoroutine(SummonGroup());
    }

    IEnumerator SummonGroup()
    {
        yield return new WaitForSeconds(8f);
        ParasiteGroup.SetActive(true);
        ParasiteGroup.transform.parent = null;
        ParasiteGroup.transform.position = new Vector3(ParasiteGroup.transform.position.x, 0, ParasiteGroup.transform.position.z);
        PlotText.GetComponent<Text>().text = "A group of parasites have spawned!";
        if (MedianAudio.clip != Resources.Load<AudioClip>("Audio/Median Audio/13"))
        {
            MedianAudio.clip = Resources.Load<AudioClip>("Audio/Median Audio/13");
            MedianAudio.Stop();
            MedianAudio.PlayOneShot(MedianAudio.clip, 1f);
        }
        StartCoroutine(MakeAlly());
    }

    IEnumerator MakeAlly()
    {
        yield return new WaitForSeconds(3f);
        LostSpirit.GetComponent<LostSpirit>().Ally = true;
        LostSpirit.GetComponent<LostSpirit>().RunSpeed = 4;
        LostSpirit.GetComponent<LostSpirit>().CurrentLocation = 0;

        if (gameObject.name == "Box & Whisker - Ice")
        {
            PlotText.GetComponent<Text>().text = "Watch as your Allied Lost Spirit uses an ice blast to freezes the parasites in place!" +
                                                "\nthis will prevent the parasites from swarming together and over powering you." +
                                                "\nPress the [ q ] key continoulsy to unleash melee attack combos and knock out the parasites!";
            if (MedianAudio.clip != Resources.Load<AudioClip>("Audio/Median Audio/14"))
            {
                MedianAudio.clip = Resources.Load<AudioClip>("Audio/Median Audio/14");
                MedianAudio.Stop();
                MedianAudio.PlayOneShot(MedianAudio.clip, 1f);
            }
        }

        if (gameObject.name == "Box & Whisker - Fire")
        {

            PlotText.GetComponent<Text>().text = "Watch as your Allied Lost Spirit uses a Fire blast to Burn the parasites over time!" +
                                                "\nthis will set the parsites on fire and cause them to be knocked out after 5 seconds of being burned!" +
                                                "\nPress the [ q ] key continoulsy to unleash melee attack combos and knock out the parasites!";
            if (MedianAudio.clip != Resources.Load<AudioClip>("Audio/Median Audio/16"))
            {
                MedianAudio.clip = Resources.Load<AudioClip>("Audio/Median Audio/16");
                MedianAudio.Stop();
                MedianAudio.PlayOneShot(MedianAudio.clip, 1f);
            }
        }


        if (gameObject.name == "Box & Whisker - Poison")
        {
            PlotText.GetComponent<Text>().text = "Watch as your Allied Lost Spirit uses a poison blast to confuse the parasites!" +
                                                "\nthis will cause the parasites to turn on each other and knock each other out" +
                                                "\nPress the [ q ] key continoulsy to unleash melee attack combos and knock out the parasites!";
            if (MedianAudio.clip != Resources.Load<AudioClip>("Audio/Median Audio/17"))
            {
                MedianAudio.clip = Resources.Load<AudioClip>("Audio/Median Audio/17");
                MedianAudio.Stop();
                MedianAudio.PlayOneShot(MedianAudio.clip, 1f);
            }
        }

        if (gameObject.name == "Box & Whisker - Electric")
        {

            PlotText.GetComponent<Text>().text = "Watch as your Allied Lost Spirit uses an Electric blast to Electricute the parasites!" +
                                                "\nthis will blast the parasites straight up in the air and electricute them untill they're knocked out!" +
                                                "\nPress the [ q ] key continoulsy to unleash melee attack combos and knock out the parasites!";
            if (MedianAudio.clip != Resources.Load<AudioClip>("Audio/Median Audio/18"))
            {
                MedianAudio.clip = Resources.Load<AudioClip>("Audio/Median Audio/18");
                MedianAudio.Stop();
                MedianAudio.PlayOneShot(MedianAudio.clip, 1f);
            }
        }

        StartCoroutine(ClearText());


    }


    IEnumerator ClearText()
    {
        yield return new WaitForSeconds(20);
        PlotText.GetComponent<Text>().text = " ";
    }

    IEnumerator ActivateUpperMarker(float time)
    {
        yield return new WaitForSeconds(time);
        UpperMarker.SetActive(true);

        MedianAudio.clip = Resources.Load<AudioClip>("Audio/Median Audio/Merge Audio");
        MedianAudio.PlayOneShot(MedianAudio.clip, 1f);
    }

    IEnumerator ActivateLowerMarker(float time)
    {
        yield return new WaitForSeconds(time);
        LowerMarker.SetActive(true);
        UpperMarker.SetActive(false);

        MedianAudio.clip = Resources.Load<AudioClip>("Audio/Median Audio/Merge Audio");
        MedianAudio.PlayOneShot(MedianAudio.clip, 1f);
    }

    IEnumerator DelayFindOutlier(float time)
    {
        yield return new WaitForSeconds(time);

        if (gameObject.name == "Box & Whisker - Poison")
        {
            PlotText.GetComponent<Text>().text = "It looks like one of the Lost Spirits had a weight of 93 pounds." +
                                               "\nThis weight is outside of the range and is therefore an outlier." +
                                                "\nthe 93 has been ploted to the outside of the Box and whisker plot" +
                                                "\nand 47 is plotted as the new max. This is because 47 is the highest number" +
                                                "\nwithin in the range while 93 is outside of the range.";



            if (MedianAudio.clip != Resources.Load<AudioClip>("Audio/Median Audio/25"))
            {
                MedianAudio.clip = Resources.Load<AudioClip>("Audio/Median Audio/25");
                MedianAudio.Stop();
                MedianAudio.PlayOneShot(MedianAudio.clip, 1f);

                MedianAudio.clip = Resources.Load<AudioClip>("Audio/Median Audio/Merge Audio");
                MedianAudio.PlayOneShot(MedianAudio.clip, 1f);
            }

            Outlier.SetActive(true);
            StartCoroutine(SummonLostSpirit(17.5f));
        }

        if (gameObject.name == "Box & Whisker - Electric")
        {
            PlotText.GetComponent<Text>().text = "It looks like one of the Lost Spirits had a weight of 89 pounds." +
                                               "\nThis weight is outside of the range and is therefore an outlier." +
                                                "\nthe 89 has been ploted to the outside of the Box and whisker plot" +
                                                "\nand 51 is plotted as the new max. This is because 51 is the highest number" +
                                                "\nwithin in the range while 93 is outside of the range.";



            if (MedianAudio.clip != Resources.Load<AudioClip>("Audio/Median Audio/26"))
            {
                MedianAudio.clip = Resources.Load<AudioClip>("Audio/Median Audio/26");
                MedianAudio.Stop();
                MedianAudio.PlayOneShot(MedianAudio.clip, 1f);

                MedianAudio.clip = Resources.Load<AudioClip>("Audio/Median Audio/Merge Audio");
                MedianAudio.PlayOneShot(MedianAudio.clip, 1f);
            }

            Outlier.SetActive(true);
            StartCoroutine(SummonLostSpirit(17.5f));
        }
    }

    IEnumerator SummonLostSpirit(float time)
    {
        yield return new WaitForSeconds(time);
        OutlierFound = true;

        PlotText.GetComponent<Text>().text = "now that you've completd the box and whisker Plot, you can summon a Lost Spirit as you ally by pressing the [ g ] key";

        if (MedianAudio.clip != Resources.Load<AudioClip>("Audio/Median Audio/10"))
        {
            MedianAudio.clip = Resources.Load<AudioClip>("Audio/Median Audio/10");
            MedianAudio.Stop();
            MedianAudio.PlayOneShot(MedianAudio.clip, 1f);
        }
    }
}