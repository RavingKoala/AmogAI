namespace AmogAI.FuzzyLogic {
	public class LeftShoulderFuzzySet : FuzzySet {
		private readonly double _peakPoint;
		private readonly double _leftOffset;
		private readonly double _rightOffset;

		public LeftShoulderFuzzySet(double peak, double right, double left) : base((peak - left + peak) / 2) {
			_peakPoint = peak;
			_rightOffset = right;
			_leftOffset = left;
		}

		public override double CalculateDOM(double val) {
			// Tests if the left or right offsets are zero, to prevent a divide by zero error
			if ((_rightOffset == 0.0 && _peakPoint == val) || (_leftOffset == 0.0 && _peakPoint == val)) {
				return 1.0;
			}

			// Find DOM if value is right of center
			else if ((val >= _peakPoint) && val < (_peakPoint + _rightOffset)) {
				double grad = 1.0 / -_rightOffset;

				return grad * (val - _peakPoint) + 1.0;
			}

			// Find DOM if value is left of center
			else if (val < _peakPoint && val >= _peakPoint - _leftOffset) {
				return 1.0;
			} else {
				// Out of range, return zero
				return 0.0;
			}
		}
	}
}
