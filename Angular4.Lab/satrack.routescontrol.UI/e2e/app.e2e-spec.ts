import { Satrack.Routescontrol.UIPage } from './app.po';

describe('satrack.routescontrol.ui App', () => {
  let page: Satrack.Routescontrol.UIPage;

  beforeEach(() => {
    page = new Satrack.Routescontrol.UIPage();
  });

  it('should display welcome message', () => {
    page.navigateTo();
    expect(page.getParagraphText()).toEqual('Welcome to app!');
  });
});
