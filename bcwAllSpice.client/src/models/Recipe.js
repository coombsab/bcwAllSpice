export class Recipe {
  constructor(data) {
    this.id = data.id
    this.title = data.title
    this.subtitle = data.subtitle
    this.instructions = data.instructions
    this.img = data.img
    this.category = data.category
    this.creatorId = data.creatorId
    this.creator = data.creator
    this.favoriteId = data.favoriteId
    this.favoritees = data.favoritees
    this.favoriteeIds = data.favoriteeIds
  }
}