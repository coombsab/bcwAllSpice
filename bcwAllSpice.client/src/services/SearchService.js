import { AppState } from "../AppState"

class SearchService {
  saveSearch(search) {
    AppState.search = search
  }
}

export const searchService = new SearchService()