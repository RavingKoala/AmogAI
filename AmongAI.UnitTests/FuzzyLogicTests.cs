using System;
using AmogAI.FuzzyLogic;
using NUnit.Framework;

namespace AmogAI.Tests;

[TestFixture]
public class FuzzyLogicTests {
	private FuzzyModule _fm;

	private FzSet _targetClose;
	private FzSet _targetMedium;
	private FzSet _targetFar;

	private FzSet _veryDesirable;
	private FzSet _desirable;
	private FzSet _undesirable;

	private FzSet _ammoLoads;
	private FzSet _ammoOkay;
	private FzSet _ammoLow;

	[SetUp]
	public void Setup() {
		// Init FuzzyModule with the fuzzyvariables and fuzzyrules
		_fm = new FuzzyModule();
		FuzzyVariable distToTarget = _fm.CreateFLV("DistanceToTarget");

		_targetClose = distToTarget.AddLeftShoulderSet("Target_Close", 0, 25, 150);
		_targetMedium = distToTarget.AddTriangularSet("Target_Medium", 25, 150, 300);
		_targetFar = distToTarget.AddRightShoulderSet("Target_Far", 150, 300, 500);

		FuzzyVariable desirability = _fm.CreateFLV("Desirability");

		_veryDesirable = desirability.AddRightShoulderSet("VeryDesirable", 50, 75, 100);
		_desirable = desirability.AddTriangularSet("Desirable", 25, 50, 75);
		_undesirable = desirability.AddLeftShoulderSet("Undesirable", 0, 25, 50);

		FuzzyVariable AmmoStatus = _fm.CreateFLV("AmmoStatus");

		_ammoLoads = AmmoStatus.AddRightShoulderSet("Ammo_Loads", 15, 30, 100);
		_ammoOkay = AmmoStatus.AddTriangularSet("Ammo_Okay", 0, 10, 30);
		_ammoLow = AmmoStatus.AddTriangularSet("Ammo_Low", 0, 0, 10);
	}

	[Test]
	public void Desirability200Distance8AmmoWithAllRules() {
		_fm.AddRule(new FzAND(_targetClose, _ammoLoads), _undesirable);
		_fm.AddRule(new FzAND(_targetClose, _ammoOkay), _undesirable);
		_fm.AddRule(new FzAND(_targetClose, _ammoLow), _undesirable);

		_fm.AddRule(new FzAND(_targetMedium, _ammoLoads), _veryDesirable);
		_fm.AddRule(new FzAND(_targetMedium, _ammoOkay), _veryDesirable);
		_fm.AddRule(new FzAND(_targetMedium, _ammoLow), _desirable);

		_fm.AddRule(new FzAND(_targetFar, _ammoLoads), _desirable);
		_fm.AddRule(new FzAND(_targetFar, _ammoOkay), _undesirable);
		_fm.AddRule(new FzAND(_targetFar, _ammoLow), _undesirable);

		double ammo = 8;
		double distance = 200;

		_fm.Fuzzify("DistanceToTarget", distance);
		_fm.Fuzzify("AmmoStatus", ammo);

		double LastDesirabilityScore = _fm.DeFuzzify("Desirability", FuzzyModule.DefuzzifyMethod.max_av);

		double desirableDom = _desirable.GetDOM();
		double undesirableDom = _undesirable.GetDOM();
		double veryDesirableDom = _veryDesirable.GetDOM();

		Assert.That((int)LastDesirabilityScore, Is.EqualTo(60));

		Assert.That(Math.Round(desirableDom, 2), Is.EqualTo(0.20));
		Assert.That(Math.Round(undesirableDom, 2), Is.EqualTo(0.33));
		Assert.That(Math.Round(veryDesirableDom, 2), Is.EqualTo(0.67));
	}

	[Test]
	public void Desirability200Distance8AmmoWithAllTargetCloseRules() {
		_fm.AddRule(new FzAND(_targetClose, _ammoLoads), _undesirable);
		_fm.AddRule(new FzAND(_targetClose, _ammoOkay), _undesirable);
		_fm.AddRule(new FzAND(_targetClose, _ammoLow), _undesirable);

		double ammo = 8;
		double distance = 200;

		_fm.Fuzzify("DistanceToTarget", distance);
		_fm.Fuzzify("AmmoStatus", ammo);

		double LastDesirabilityScore = _fm.DeFuzzify("Desirability", FuzzyModule.DefuzzifyMethod.max_av);

		double desirableDom = _desirable.GetDOM();
		double undesirableDom = _undesirable.GetDOM();
		double veryDesirableDom = _veryDesirable.GetDOM();

		Assert.That((int)LastDesirabilityScore, Is.EqualTo(0));

		Assert.That(Math.Round(desirableDom, 2), Is.EqualTo(0));
		Assert.That(Math.Round(undesirableDom, 2), Is.EqualTo(0));
		Assert.That(Math.Round(veryDesirableDom, 2), Is.EqualTo(0));
	}

	[Test]
	public void Desirability200Distance8AmmoWithAmmoOkayRules() {
		_fm.AddRule(new FzAND(_targetClose, _ammoOkay), _undesirable);

		_fm.AddRule(new FzAND(_targetMedium, _ammoOkay), _veryDesirable);

		_fm.AddRule(new FzAND(_targetFar, _ammoOkay), _undesirable);

		double ammo = 8;
		double distance = 200;

		_fm.Fuzzify("DistanceToTarget", distance);
		_fm.Fuzzify("AmmoStatus", ammo);

		double LastDesirabilityScore = _fm.DeFuzzify("Desirability", FuzzyModule.DefuzzifyMethod.max_av);

		double desirableDom = _desirable.GetDOM();
		double undesirableDom = _undesirable.GetDOM();
		double veryDesirableDom = _veryDesirable.GetDOM();

		Assert.That((int)LastDesirabilityScore, Is.EqualTo(62));

		Assert.That(Math.Round(desirableDom, 2), Is.EqualTo(0));
		Assert.That(Math.Round(undesirableDom, 2), Is.EqualTo(0.33));
		Assert.That(Math.Round(veryDesirableDom, 2), Is.EqualTo(0.67));
	}

	[Test]
	public void Desirability200Distance8AmmoWithTargetFarRules() {
		_fm.AddRule(new FzAND(_targetFar, _ammoLoads), _desirable);
		_fm.AddRule(new FzAND(_targetFar, _ammoOkay), _undesirable);
		_fm.AddRule(new FzAND(_targetFar, _ammoLow), _undesirable);

		double ammo = 8;
		double distance = 200;

		_fm.Fuzzify("DistanceToTarget", distance);
		_fm.Fuzzify("AmmoStatus", ammo);

		double LastDesirabilityScore = _fm.DeFuzzify("Desirability", FuzzyModule.DefuzzifyMethod.max_av);

		double desirableDom = _desirable.GetDOM();
		double undesirableDom = _undesirable.GetDOM();
		double veryDesirableDom = _veryDesirable.GetDOM();

		Assert.That((int)LastDesirabilityScore, Is.EqualTo(12));

		Assert.That(Math.Round(desirableDom, 2), Is.EqualTo(0));
		Assert.That(Math.Round(undesirableDom, 2), Is.EqualTo(0.33));
		Assert.That(Math.Round(veryDesirableDom, 2), Is.EqualTo(0));
	}
}