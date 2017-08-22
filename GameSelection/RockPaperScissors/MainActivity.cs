using System;
using Android.App;
using Android.Widget;
using Android.OS;
using Android.Content;
using Android.Runtime;
using Android.Views;


namespace RockPaperScissors
{
    [Activity(Label = "RockPaperScissors", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {

        #region 0 - Class fields ( declaration )

        ImageButton ibRock, ibPaper, ibScissors;
        Button bReset, bClose;
        TextView tvPlayerScore, tvCPUScore;
        ImageView ivCPUChoice, ivPlayer;
        Random rnd;
        int cpuChoice, playerScore, cpuScore;

        #endregion

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Set our view from the "main" layout resource
             SetContentView (Resource.Layout.Main);

            #region 0 - Class fields ( initialization )

            ibRock = FindViewById<ImageButton>(Resource.Id.ibRock);
            ibPaper = FindViewById<ImageButton>(Resource.Id.ibPaper);
            ibScissors = FindViewById<ImageButton>(Resource.Id.ibScissors);
            ivCPUChoice = FindViewById<ImageView>(Resource.Id.ivCPUChoice);
            ivPlayer = FindViewById<ImageView>(Resource.Id.ivPlayer);
            tvPlayerScore = FindViewById<TextView>(Resource.Id.tvPlayerScore);
            tvCPUScore = FindViewById<TextView>(Resource.Id.tvCPUScore);
            bReset = FindViewById<Button>(Resource.Id.bReset);
            bClose = FindViewById<Button>(Resource.Id.bClose);
            rnd = new Random();

            #endregion

            #region 1 - Play ( event handlers )

            ibRock.Click += Play;
            ibPaper.Click += Play;
            ibScissors.Click += Play;

            #endregion

            #region 2 - Lambda expressions

            // Syntax for a lambda:
            // ( TypeA a, TypeB b, ... ) => { statements };
            bReset.Click += (object sender, EventArgs e) =>
            {
                playerScore = cpuScore = 0;
                tvPlayerScore.Text = "Player: 0";
                tvCPUScore.Text = "CPU: 0 ";
            }
            ;

            // Syntax for a lambda:
            // ( a, b, ... ) => single_statement;
            bClose.Click += (s, e) => Finish();

            #endregion

            #region 5 - Implicit Intent ( Sending a request )

            ivPlayer.Click += (s, e) =>
            {
                // "android.media.action.IMAGE_CAPTURE"
                Intent cameraIntent = new Intent(Android.Provider.MediaStore.ActionImageCapture);
                StartActivityForResult(cameraIntent, 0);
            }
            ;

            #endregion
        }

        #region 1 - Play ( method )

        void Play(object sender, EventArgs e)
        {
            cpuChoice = rnd.Next(3);
            /*
             * 0: rock
             * 1: paper
             * 2: scissors
             */
            switch (cpuChoice)
            {
                case 0:
                    #region CPU --> Rock

                    ivCPUChoice.SetImageResource(Resource.Drawable.rock);
                    if (sender == ibRock)
                    {
                        Toast.MakeText(this.ApplicationContext, "That was a draw!", ToastLength.Short).Show();
                    }
                    else if (sender == ibPaper)
                    {
                        playerScore++;
                        tvPlayerScore.Text = "Player: " + playerScore;
                    }
                    else
                    {
                        cpuScore++;
                        tvCPUScore.Text = "CPU: " + cpuScore;
                    }
                    break;

                #endregion

                case 1:
                    #region CPU --> Paper

                    ivCPUChoice.SetImageResource(Resource.Drawable.paper);
                    if (sender == ibRock)
                    {
                        cpuScore++;
                        tvCPUScore.Text = "CPU: " + cpuScore;
                    }
                    else if (sender == ibPaper)
                    {
                        Toast.MakeText(this.ApplicationContext, "That was a draw!", ToastLength.Short).Show();
                    }
                    else
                    {
                        playerScore++;
                        tvPlayerScore.Text = "Player: " + playerScore;
                    }
                    break;

                #endregion

                case 2:
                    #region CPU --> Scissors

                    ivCPUChoice.SetImageResource(Resource.Drawable.scissors);
                    if (sender == ibRock)
                    {
                        playerScore++;
                        tvPlayerScore.Text = "Player: " + playerScore;
                    }
                    else if (sender == ibPaper)
                    {
                        cpuScore++;
                        tvCPUScore.Text = "CPU: " + cpuScore;
                    }
                    else
                    {
                        Toast.MakeText(this.ApplicationContext, "That was a draw!", ToastLength.Short).Show();
                    }
                    break;

                    #endregion
            }
        }

        #endregion

        #region 3 - Activity Life Cycle

        protected override void OnStart()
        {
            base.OnStart();
            Toast.MakeText(this.ApplicationContext, "OnStart()", ToastLength.Short).Show();
        }

        protected override void OnStop()
        {
            base.OnStop();
            Toast.MakeText(this.ApplicationContext, "OnStop()", ToastLength.Short).Show();
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();
            Toast.MakeText(this.ApplicationContext, "OnDestroy()", ToastLength.Short).Show();
        }

        #endregion

        #region 4 - Maintaining State

        protected override void OnSaveInstanceState(Bundle outState)
        {
            outState.PutInt("player_score", playerScore);
            outState.PutInt("cpu_score", cpuScore);
            base.OnSaveInstanceState(outState);
        }

        protected override void OnRestoreInstanceState(Bundle savedInstanceState)
        {
            base.OnRestoreInstanceState(savedInstanceState);
            playerScore = savedInstanceState.GetInt("player_score");
            cpuScore = savedInstanceState.GetInt("cpu_score");
            tvPlayerScore.Text = "Player: " + playerScore;
            tvCPUScore.Text = "CPU: " + cpuScore;
        }

        #endregion

        #region 5 - Implicit Intent ( Recieving the image )

        protected override void OnActivityResult(int requestCode, Result resultCode, Intent data)
        {
            base.OnActivityResult(requestCode, resultCode, data);

            if (requestCode == 0 && resultCode == Result.Ok)
            {
                Android.Graphics.Bitmap bitmap = (Android.Graphics.Bitmap)data.Extras.Get("data");
                ivPlayer.SetImageBitmap(bitmap);
            }
        }

        #endregion
    }
}

