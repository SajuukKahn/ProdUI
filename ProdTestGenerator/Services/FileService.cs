namespace ProdTestGenerator.Services
{
    using ProductionCore.Interfaces;
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Windows.Media.Imaging;

    /// <summary>
    /// Defines the <see cref="FileService" />.
    /// </summary>
    public class FileService : IFileService
    {
        private readonly ICardFactory _cardFactory;

        private readonly ICardSubStepFactory _cardSubStepFactory;

        private readonly IPlaybackService _playbackService;

        public FileService(ICardFactory cardFactory, ICardSubStepFactory cardSubStepFactory, IPlaybackService playbackService)
        {
            _cardFactory = cardFactory;
            _cardSubStepFactory = cardSubStepFactory;
            _playbackService = playbackService;
        }

        /// <summary>
        /// The SaveToJSON.
        /// </summary>
        /// <param name="program">The program<see cref="IProgramData"/>.</param>
        public void SaveToJSON(IProgramData program)
        {
        }

        public void RetrieveProgramSteps(IProgramData programData)
        {
            string[] titleArray = { "PolyLine 3D", "Area", "Move", "Line", "PolyLine", "Arc", "Spiral", "Rectangular Sprial", "Dot", "Part Presense Check" };

            BitmapImage? image = new BitmapImage(new Uri(Environment.CurrentDirectory + "\\Modules\\TestImages\\FID.bmp", UriKind.RelativeOrAbsolute));

            if (programData?.ProgramName == "Manual Placement Simulation")
            {
                List<ICardSubStep> steplist = new List<ICardSubStep>();

                var step = ;
                step.SubStepName = "Place Product";
                step.SubStepData = new string[] { string.Empty };


                ICard zerothCardModel = _cardFactory.Create();
                zerothCardModel.StepTitle = "Place Product";
                zerothCardModel.StepImage = programData.ProductImage;
                zerothCardModel.StepModalData = new ModalData()
                {
                    CanAbort = true,
                    CanContinue = true,
                    CanRetry = false,
                    Card = null,
                    Instructions = "Place Product and press 'Continue' to begin" + Environment.NewLine + "Press 'Abort' to exit path playback",
                    InstructionImage = programData.ProductImage,
                    IsError = false,
                };

                zerothCardModel.CardSubSteps?.Add(_cardSubStepFactory.Create());


                _playbackService!.ProgramSteps?.Add(zerothCardModel);
            }

            List<CardSubStep> fidList = new List<CardSubStep>
            {
                new CardSubStep("Fiducial A", RandomCoordinates()),
                new CardSubStep("Fiducial B", RandomCoordinates()),
            };

            Card firstCardModel = new Card()
            {
                CardSubSteps = fidList,
                StepTitle = "Fiducial Check",
                StepImage = image,
                StepModalData = new ModalData() { CanAbort = true, CanContinue = true, CanRetry = true, Card = null, Instructions = "Fiducials failed, Select an option below", InstructionImage = image },
            };

            cardDeck.Add(firstCardModel);
            int randSize = new Random().Next(2 * _scaleDataSet, 8 * _scaleDataSet);
            List<CardSubStep> surfaceList = new List<CardSubStep>();
            for (int i = 0; i < randSize; i++)
            {
                surfaceList.Add(new CardSubStep("Surface " + (i + 1), RandomCoordinates()));
            }

            Card secondCardModel = new Card()
            {
                CardSubSteps = surfaceList,
                StepTitle = "Surface Height Check",
                StepModalData = new ModalData() { CanAbort = true, CanRetry = true, Card = null, Instructions = "Surface Height Checks failed, Select an option below" },
            };
            cardDeck.Add(secondCardModel);

            randSize = new Random().Next(4 * _scaleDataSet, 12 * _scaleDataSet);
            for (int i = 0; i < randSize; i++)
            {
                string stepTitle = titleArray[new Random().Next(titleArray.Length)];
                int randSteps = new Random().Next(1 * _scaleDataSet, 8 * _scaleDataSet);
                List<CardSubStep> randomSteps = new List<CardSubStep>();

                for (int j = 0; j < randSteps; j++)
                {
                    randomSteps.Add(new CardSubStep(stepTitle + (j + 1), RandomCoordinates()));
                }

                Card cardModel = new Card()
                {
                    CardSubSteps = randomSteps,
                    StepTitle = stepTitle,
                };

                if (new Random().Next(0, 4) == 1)
                {
                    image = new BitmapImage(new Uri(Environment.CurrentDirectory + "\\Modules\\TestImages\\PCB" + new Random().Next(1, 9).ToString() + ".bmp", UriKind.RelativeOrAbsolute));
                    cardModel.StepImage = image;
                }

                if (cardModel.StepTitle == "Part Presence Check")
                {
                    cardModel.StepModalData = new ModalData() { CanAbort = true, CanContinue = true, CanRetry = true, Card = null, Instructions = cardModel.StepTitle + " failed, Select an option below" };
                }

                cardDeck.Add(cardModel);
            }
        }
}
