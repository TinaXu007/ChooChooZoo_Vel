using UnityEngine;

namespace KartGame.KartSystems {

    public class KeyboardInput : BaseInput
    {
        public string TurnInputName = "Horizontal";
        public string AccelerateButtonName = "Accelerate";
        public string BrakeButtonName = "Brake";

        public override InputData GenerateInput() {
            return new InputData
            {
                arduinoController = ArduinoConnector.Instance.receivedString.Split(',').ToList();
                // moveInput = float.Parse(arduinoController[0])/2;
                TurnInput = float.Parse(arduinoController[1]);
                
                Accelerate = Input.GetButton(AccelerateButtonName),
                Brake = Input.GetButton(BrakeButtonName),
                // TurnInput = Input.GetAxis("Horizontal")
            };
        }
    }
}
