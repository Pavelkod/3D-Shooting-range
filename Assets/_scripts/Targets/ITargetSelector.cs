using System.Collections.Generic;

public interface ITargetSelector
{
    Target GetTarget(List<Target> targets);
}

