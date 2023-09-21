namespace AmogAI.FuzzyLogic; 
	public class TriangleFuzzySet : FuzzySet {
		private readonly double _peakPoint;
		private readonly double _leftOffset;
		private readonly double _rightOffset;

		public TriangleFuzzySet(double mid, double left, double right) : base(mid) {
			_peakPoint = mid;
			_leftOffset = left;
			_rightOffset = right;
		}

		public override double CalculateDOM(double val) {
			// Tests if the left or right offsets are zero, to prevent a divide by zero error
			if (_rightOffset == 0.0 && _peakPoint == val || _leftOffset == 0.0 && _peakPoint == val) {
				return 1.0;
			}

			// Find DOM if value is left of the center
			if (val <= _peakPoint && val >= _peakPoint - _leftOffset) {
				double grad = 1.0 / _leftOffset;

				return grad * (val - (_peakPoint - _leftOffset));
			}

			// Find DOM if value is right of center
			if (val > _peakPoint && val < _peakPoint + _rightOffset) {
				double grad = 1.0 / -_rightOffset;

				return grad * (val - _peakPoint) + 1.0;
			}

			// Out of range, return zero
			return 0.0;
		}
	}
