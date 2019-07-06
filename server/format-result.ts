import { ShortestPathResults, isShortestPathNodes, ShortestPathError } from "../shared/shortest-path";

export function formatResult(pathResult: ShortestPathResults): string {
	if (isShortestPathNodes(pathResult)) {
		return pathResult.nodes.length === 0
			? 'No Route'
			: pathResult.nodes.join(' -> ');
	} else {
		switch (pathResult) {
			case ShortestPathError.Invalid:
				return 'Invalid Origin And Destination';
			case ShortestPathError.InvalidOrigin:
				return 'Invalid Origin';
			case ShortestPathError.InvalidDestination:
				return 'Invalid Destination';
			default: // Serious error in application logic.
				throw new Error('Unexpected ShortestPathError object received.');
		}
	}
}